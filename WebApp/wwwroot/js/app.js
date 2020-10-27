window.clipboardCopy = {
    copyText: function (text) {
        navigator.clipboard.writeText(text).then(function () {
            alert("Copied '" + text + "'.");
        })
            .catch(function (error) {
                alert(error);
            });
    }
};
