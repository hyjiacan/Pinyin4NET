$(function () {
    $('input[type=text]').keypress(function (e) {
        var me = $(this);
        var py = me.parent().siblings('.input-group').find('input[name=output]');
        if (e.keyCode == 13) {
            var key = me.val().trim();

            $.post('#', {
                dt: new Date().getTime(),
                key: key,
                cmd: me.attr('data-cmd'),
                multi: $('#multi').val(),
                casetype: $('#caseType').val(),
                tonetype: $('#toneType').val(),
                vtype: $('#vType').val()
            }, function (pinyin) {
                py.val(pinyin);
            });
        }
    });
});