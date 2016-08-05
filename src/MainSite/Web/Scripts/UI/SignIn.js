var SignIn = new function () {
    this.ShowPasswordHint = function () {
        jQuery('#hintEmailAddress').val(jQuery('#userName').val());
        var submitOptions = { target: '#passwordHintSection' };
        jQuery('#showPasswordHintForm').hide();
        jQuery('#showPasswordHintForm').ajaxSubmit(submitOptions);
    };

    this.SubmitLogin = function () {
        jQuery('#loginForm').Submit();
    };
}