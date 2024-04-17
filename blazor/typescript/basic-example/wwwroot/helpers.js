var helpers;
(function (helpers) {
    var Logger = /** @class */ (function () {
        function Logger() {
        }
        Logger.prototype.log = function (text) {
            console.log(text);
        };
        return Logger;
    }());
    function getLogger() {
        return new Logger();
    }
    helpers.getLogger = getLogger;
})(helpers || (helpers = {}));
//# sourceMappingURL=helpers.js.map