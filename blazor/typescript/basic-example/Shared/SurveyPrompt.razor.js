"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.GetInstance = void 0;
var SurveyPrompt = /** @class */ (function () {
    function SurveyPrompt() {
    }
    SurveyPrompt.prototype.displayAlert = function (message) {
        alert(message);
    };
    SurveyPrompt.prototype.displayPrompt = function (message, defaultValue) {
        return prompt(message, defaultValue);
    };
    SurveyPrompt.prototype.log = function (message) {
        console.log(message);
    };
    return SurveyPrompt;
}());
function GetInstance() {
    return new SurveyPrompt();
}
exports.GetInstance = GetInstance;
//# sourceMappingURL=SurveyPrompt.razor.js.map