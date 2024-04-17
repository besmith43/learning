class SurveyPrompt {
    public displayAlert(message: string) {
        alert(message);
    }

    public displayPrompt(message: string, defaultValue: string) {
        return prompt(message, defaultValue);
    }

    public log(message: string) {
        console.log(message);
    }
}

export function GetInstance() {
    return new SurveyPrompt();
}