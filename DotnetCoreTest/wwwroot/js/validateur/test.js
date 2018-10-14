$(function () {
    jQuery.validator.addMethod('maxamount',
        function (value, element, params) {
            // custom validation code 
            return false;
        }, ''
    );
    jQuery.validator.unobtrusive.adapters.add(
        'maxamount',
        function (options) {
            options.rules['maxamount'] = {};
            options.messages['maxamount'] = options.message;
        });
}(jQuery));