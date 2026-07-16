// ============ GLOBALS ============
let ytPlayer = null;

$(function () {
    loadYouTubeApi();
    initYouTubePlayer(); // pehli load ke liye


    // ===== SubCourse Tab Click (AJAX - No Reload) =====
    $(document).on('click', '.subcourse-tab', function () {
        const isPurchased = $(this).data('purchased');
        const subCourseId = $(this).data('subcourse-id');

        if (!isPurchased) {
            alert('This Course IS Not Purchased Yet.....');
            return;
        }

        if ($(this).hasClass('active')) return; // already khula hua hai

        $.get('/MyCourse/SwitchSubCourse', { subCourseId: subCourseId })
            .done(function (html) {
                $('#courseContentWrapper').html(html);
                $('.subcourse-tab').removeClass('active');
                $('.subcourse-tab[data-subcourse-id="' + subCourseId + '"]').addClass('active');

                initYouTubePlayer(); // naya iframe ke liye YouTube API dobara init
            })
            .fail(function () {
                alert('Not Able To Load SubCourse.');
            });
    });




    // ===== Sidebar: Unlocked lesson par click =====
    $(document).on('click', '.lesson-item.unlocked', function () {
        const lessonId = $(this).data('lesson-id');
        loadLesson(lessonId);
    });

    // ===== Locked lesson par click - warning =====
    $(document).on('click', '.lesson-item.locked', function () {
        alert('This Lesson is locked. First Complete Previous Lesson Video + 5 MCQ + Assignment.');
    });

    // ===== MCQ Button Click - Modal Open =====
    $(document).on('click', '#btnOpenMcq', function () {
        const lessonId = $('#lessonPlayerWrapper').data('lesson-id');

        $.get('/MyCourse/GetMcqQuestions', { lessonId: lessonId })
            .done(function (html) {
                $('#mcqModalContainer').html(html);
                const modal = new bootstrap.Modal(document.getElementById('mcqModal'));
                modal.show();
            })
            .fail(function () {
                alert('MCQ Not Loaded. Please Try Again.');
            });
    });

    // ===== MCQ Submit =====
    $(document).on('click', '#btnSubmitMcq', function () {
        const lessonId = $('#lessonPlayerWrapper').data('lesson-id');
        const $form = $('#mcqForm');

        // Validation: sabhi 5 questions answer kiye hain ya nahi
        const totalQuestions = $form.find('.mcq-question').length;
        const answeredCount = $form.find('input[type=radio]:checked').length;

        if (answeredCount < totalQuestions) {
            alert('Please Answer All ' + totalQuestions + ' Questions.');
            return;
        }

        const answers = [];
        $form.find('.mcq-question').each(function () {
            const questionId = $(this).data('question-id');
            const selectedOptionId = $(this).find('input[type=radio]:checked').val();
            answers.push({ questionId: questionId, selectedOptionId: parseInt(selectedOptionId) });
        });

        const token = $form.find('input[name="__RequestVerificationToken"]').val();

        $.ajax({
            url: '/MyCourse/SubmitMcq',
            type: 'POST',
            contentType: 'application/json',
            headers: { 'RequestVerificationToken': token },
            data: JSON.stringify({ lessonId: lessonId, answers: answers }),
            success: function (result) {
                const alertClass = result.isPassed ? 'alert-success' : 'alert-danger';
                $('#mcqResultAlert').html(
                    '<div class="alert ' + alertClass + '">' + result.message + '</div>'
                );

                if (result.isPassed) {
                    setTimeout(function () {
                        bootstrap.Modal.getInstance(document.getElementById('mcqModal')).hide();
                        // MCQ button hide, Assignment section reveal
                        $('#mcqButtonArea').addClass('d-none');
                        $('#assignmentArea').removeClass('d-none');
                    }, 1200);
                }
            },
            error: function (xhr) {
                alert(xhr.responseJSON?.message || 'Something is wrong please Try Again.');
            }
        });
    });

    // ===== Assignment Submit (Step 6 - Full Logic) =====
    $(document).on('submit', '#assignmentForm', function (e) {
        e.preventDefault();

        const $form = $(this);
        const lessonId = $form.find('input[name="lessonId"]').val();
        const fileInput = $form.find('input[type="file"]')[0];

        if (!fileInput.files.length) {
            alert('Please Select A File.');
            return;
        }

        // File size check (10MB) - client side pre-check, backend bhi check karta hai
        if (fileInput.files[0].size > 10 * 1024 * 1024) {
            alert('File Size Should Not Be Greater Than 10MB.');
            return;
        }

        const formData = new FormData();
        formData.append('lessonId', lessonId);
        formData.append('file', fileInput.files[0]);

        const token = $form.find('input[name="__RequestVerificationToken"]').val();
        const $submitBtn = $form.find('button[type="submit"]');

        $submitBtn.prop('disabled', true).text('Uploading...');

        $.ajax({
            url: '/MyCourse/SubmitAssignment',
            type: 'POST',
            data: formData,
            processData: false,   // FormData ko jQuery serialize na kare
            contentType: false,   // browser khud multipart boundary set karega
            headers: { 'RequestVerificationToken': token },
            success: function (result) {
                if (result.success) {
                    // 1. Assignment area mein success message dikhao, form hata do
                    $('#assignmentArea').html(
                        '<h5>Assignment Submission</h5>' +
                        '<div class="alert alert-success mb-0">' + result.message + '</div>'
                    );

                    // 2. Current lesson ko sidebar mein "Completed" mark karo
                    markCurrentLessonCompleted(lessonId);

                    // 3. Sidebar mein next lesson ko unlock karo
                    unlockNextLesson(lessonId);
                } else {
                    alert(result.message || 'Not Able TO Submit Assignment.');
                    $submitBtn.prop('disabled', false).text('Submit Assignment');
                }
            },
            error: function (xhr) {
                alert(xhr.responseJSON?.message || 'Something is wrong . Please Try Again.');
                $submitBtn.prop('disabled', false).text('Submit Assignment');
            }
        });
    });

    // ============ SIDEBAR: MARK CURRENT LESSON COMPLETED ============
    function markCurrentLessonCompleted(lessonId) {
        const $item = $('.lesson-item[data-lesson-id="' + lessonId + '"]');
        $item.find('.lesson-icon').html('<span class="icon-check">&#10003;</span>');
    }

    // ============ SIDEBAR: UNLOCK NEXT LESSON (No Reload) ============
    function unlockNextLesson(currentLessonId) {
        const $currentItem = $('.lesson-item[data-lesson-id="' + currentLessonId + '"]');
        const $nextItem = $currentItem.next('.lesson-item');

        if ($nextItem.length === 0) {
            // Yeh course ka last lesson tha - koi next nahi
            return;
        }

        // locked -> unlocked
        $nextItem.removeClass('locked').addClass('unlocked');
        $nextItem.attr('data-unlocked', 'true');
        $nextItem.find('.lesson-icon').html('<span class="icon-play">&#9658;</span>');

        // User ko batao ki agla lesson unlock ho gaya
        const nextTitle = $nextItem.find('.lesson-title').text();
        showUnlockToast(nextTitle);


        updateProgressBar();
    }

    // ===== NAYA FUNCTION =====
    function updateProgressBar() {
        const totalLessons = $('.lesson-item').length;
        const completedLessons = $('.lesson-icon .icon-check').length;
        const percentage = totalLessons > 0 ? Math.round((completedLessons / totalLessons) * 100) : 0;

        $('#completedCountText').text(completedLessons);
        $('#progressPercentText').text(percentage + '%');
        $('#courseProgressBar').css('width', percentage + '%').attr('aria-valuenow', percentage);
    }

    // ============ SIMPLE TOAST/NOTIFICATION ============
    function showUnlockToast(lessonTitle) {
        const toastHtml =
            '<div class="unlock-toast">' +
            '&#127881; "' + lessonTitle + '" Unlocked!' +
            '</div>';

        $('body').append(toastHtml);
        setTimeout(function () {
            $('.unlock-toast').fadeOut(400, function () { $(this).remove(); });
        }, 3000);
    }
});

// ============ LESSON SWITCH (AJAX - No Reload) ============
function loadLesson(lessonId) {
    $.get('/MyCourse/GetLesson', { lessonId: lessonId })
        .done(function (html) {
            $('#leftPlayerColumn').html(html);

            // Sidebar active class update
            $('.lesson-item').removeClass('active');
            $('.lesson-item[data-lesson-id="' + lessonId + '"]').addClass('active');

            // Naye iframe ke liye YouTube player dobara init karo
            initYouTubePlayer();
        })
        .fail(function (xhr) {
            alert(xhr.responseJSON?.message || 'Lesson Load Nahi Ho Saka.');
        });
}

// ============ YOUTUBE IFRAME API ============
function loadYouTubeApi() {
    if (window.YT) return; // pehle se loaded hai
    const tag = document.createElement('script');
    tag.src = "https://www.youtube.com/iframe_api";
    document.body.appendChild(tag);
}

// YouTube API is function ko khud call karta hai jab ready ho
window.onYouTubeIframeAPIReady = function () {
    initYouTubePlayer();
};

function initYouTubePlayer() {
    if (typeof YT === 'undefined' || !YT.Player) return; // API abhi load nahi hua

    const iframe = document.getElementById('youtube-player');
    if (!iframe) return;

    ytPlayer = new YT.Player('youtube-player', {
        events: {
            'onStateChange': onPlayerStateChange
        }
    });
}

function onPlayerStateChange(event) {
    if (event.data === YT.PlayerState.ENDED) {
        markVideoCompleted();
    }
}

// ============ VIDEO COMPLETE -> MCQ BUTTON SHOW ============
function markVideoCompleted() {
    const lessonId = $('#lessonPlayerWrapper').data('lesson-id');

    $.ajax({
        url: '/MyCourse/MarkVideoCompleted',
        type: 'POST',
        contentType: 'application/json',
        data: JSON.stringify({ lessonId: lessonId }),
        success: function (result) {
            if (result.success) {
                $('#mcqButtonArea').removeClass('d-none');
            }
        }
    });
}