namespace helpers {
    class Logger {
        public log(text: string) {
            console.log(text);
        }
    }

    export function getLogger(): Logger {
        return new Logger();
    }
}