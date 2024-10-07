$('.custom-file-input').on(
        'change',
    function (e) {
        var fileName = $(this)[0].files[0].name;
        var nextSibling = e.target.nextElementSibling;
        nextSibling.innerText = fileName;
    });
$('#flexSwitchCheckChecked').on(
    'change', (e) => {
        var checkbox = $('input[name="change_img"]')[0];
        if (!checkbox.checked) {
            $('#custom-file').hide();
            $('#customFile').hide();
            $('#custom-file-label').hide();
            $('.error-img').hide();
        } else {
            $('#customFile').show();
            $('#custom-file').show();
            $('#custom-file-label').show();
            $('.error-img').show();
        }
    }
)
$(document).ready((e) => {
    var checkbox = $('input[name="change_img"]')[0];
    if (!checkbox.checked) {
        $('#custom-file').hide();
        $('#customFile').hide();
        $('#custom-file-label').hide();
        $('.error-img').hide();
    } else {
        $('#customFile').show();
        $('#custom-file').show();
        $('#custom-file-label').show();
        $('.error-img').show();
    }
});