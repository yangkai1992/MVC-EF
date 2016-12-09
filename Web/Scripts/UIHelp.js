var Widget = {};


Widget.alert = function (message) {
    var agr = [message];
    Widget.multialert(agr);
}


Widget.multialert = function (messages) {
    var tmpl =
           "<div class='ui-alert'>\
                <div class='ui-alert-mask'></div>\
                <div class='ui-alert-content'>\
                    <div class='ui-alert-text'>\
                        <ul>\
                            {{each message}}\
                                <li style='list-style-type:none;'>\
                                    ${$value}\
                                </li>\
                            {{/each}}\
                        </ul>\
                    </div>\
                    <div class='ui-alert-action'>\
                        <button class='ui-alert-close btn btn-block btn-primary btn-flat'>确 认</button>\
                    </div>\
                </div>\
            </div>";
    var data = { message: messages }
    var $obj = $.tmpl(tmpl, data);
    $obj.find(".ui-alert-close").click(function () {
        $obj.fadeOut("fast", function () {
            $obj.remove();
        });
    });
    $(document.body).append($obj);

    var contentHeight = $obj.find(".ui-alert-content").outerHeight();
    var contentWidth = $obj.find(".ui-alert-content").outerWidth();

    var $win = $(window);
    var size = { height: $win.height(), width: $win.width() };

    var contentPos = {
        left: $win.scrollLeft() - contentWidth / 2 + $win.outerWidth() / 2,
        top: $win.scrollTop() - contentHeight / 2 + $win.outerHeight() / 2
    };

    if (contentHeight > size.height) {
        contentPos.top = $win.scrollTop() + 20;
    }

    $obj.find(".ui-alert-mask").css({ width: size.width, height: size.height });
    $obj.find(".ui-alert-content").css({ left: contentPos.left, top: contentPos.top });
};