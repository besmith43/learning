///usr/bin/env jbang "$0" "$@" ; exit $?
//JAVA 25+
//DEPS dev.tamboui:tamboui-toolkit:LATEST
//DEPS dev.tamboui:tamboui-panama-backend:LATEST

import dev.tamboui.toolkit.app.ToolkitRunner;
import static dev.tamboui.toolkit.Toolkit.markupText;

void main() throws Exception {
    try (var runner = ToolkitRunner.create()) {
        runner.run(() ->
            markupText("Hello, [red]TamboUI[/red]! Press [blue]q[/] to exit!")
        );
    }
}
