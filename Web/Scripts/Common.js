jQuery.fn.extend({
    validate: function () {
        return Validate(this);
    }
});

/*
* 表单检查插件
* 涉及CSS类名：
*       v-error: 出错的元素，例如input, select
* 涉及data：
*       data-v-required: 必填项
*       data-v-regexp: 正则检验
*       data-v-maxlength: 最大长度
* 消息提示data：
*       data-v-maxlength-msg: 最大长度触发的消息
*       data-v-regexp-msg: 正则触发的消息
*/
var Validate = function ($box) {
    var $allItems = $box.find("[data-v-required], [data-v-regexp], [data-v-maxlength]");
    var result = true;

    // 已处理过的条目，主要是针对checkbox, redio button的fix
    // 格式存储例如 ,food,tools,
    var handledNames = "";

    var _showMessage = function ($message, text) {
        $message.html("&nbsp;<i class='v-error-icon'></i> " + text + "&nbsp;")
            .show()
            .css("display", "inline-block");
    };
    var _hideMessage = function ($message) {
        $message.hide();
    };

    var _validateCheckbox = function (name) {
        var $items = $box.find("input[name='" + name + "']:checked");
        console.log($items.length);
        return $items.length > 0;
    };

    var _validateRadios = function (name) {
        var $item = $box.find("input[name='" + name + "']:checked");
        console.log($item.length);
        return $item.length > 0;
    };

    $allItems.each(function () {
        var $this = $(this);
        var name = $this.attr("name");
        var $message = $box.find("span[data-valmsg-for='" + name + "']");
        var val = $this.val();
        var required = $this.data("v-required");
        var regexp = $this.data("v-regexp");
        var maxlength = $this.data("v-maxlength");
        var hasText = (val != null && val != "");

        if (handledNames.indexOf("," + name + ",") != -1) {
            return;
        }

        var inputType = $this.attr("type");

        if (inputType && inputType.toLowerCase() == "checkbox") {
            handledNames += "," + name + ",";
            _hideMessage($message);

            if (!_validateCheckbox(name)) {
                _showMessage($message, required);
                result = false;
            }

            return;
        }

        if (inputType && inputType.toLowerCase() == "radio") {
            handledNames += "," + name + ",";
            _hideMessage($message);

            if (!_validateRadios(name)) {
                _showMessage($message, required);
                result = false;
            }

            return;
        }

        // 必填 
        if (required != undefined && !hasText) {
            _showMessage($message, required);
            result = false;
            $this.addClass("v-error");
            return;
        }

        // 正则
        if (hasText && regexp != undefined) {
            var rgx = new RegExp(regexp);

            if (!rgx.test(val)) {
                _showMessage($message, $this.data("v-regexp-msg"));
                result = false;
                $this.addClass("v-error");
                return;
            }
        }

        // 最大长度
        if (hasText && maxlength != undefined) {
            var maxLen = parseInt(maxlength);

            if (val.length > maxLen) {
                _showMessage($message, $this.data("v-maxlength-msg"));
                result = false;
                $this.addClass("v-error");
                return;
            }
        }

        $this.removeClass("v-error");
        _hideMessage($message);
    });

    return result;
};