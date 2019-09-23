$(function () {
    $('input[type=text]').keypress(function (e) {
        var me = $(this);
        var output = me.parent().siblings('.input-group').find('input[name=output]');
        if (e.keyCode == 13) {
            var key = me.val().trim();

            $.post('#', {
                dt: new Date().getTime(),
                key: key,
                matchAll: $('#matchAll').is(':checked'),
                cmd: me.attr('data-cmd'),
                multi: $('#multi').val(),
                casetype: $('#caseType').val(),
                tonetype: $('#toneType').val(),
                vtype: $('#vType').val()
            }, function (pinyin) {
                output.val(pinyin);
            });
        }
    });
});