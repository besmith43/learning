import QtQuick 2.9
import QtQuick.Layouts 1.3
import QtQuick.Controls 2.3
import QtQuick.Controls.Material 2.1

ApplicationWindow {
    id: window
    width: 360
    height: 520
    visible: true
    title: "First Attempt"

    Material.theme: Material.Light

    Button {
        x: 50
        y: 350
        text: "Go to Test"
        onClicked: loader.show("test.qml")
    }

    /* 
        The important thing to note in this example is that the main.qml will be reloaded evertime that the back to main button is clicked
        because of this and the fact that the main.qml is never unloaded, only place things in the main.qml that you want to be permenantly visible
        additionally if you use the stackview this isn't an issue because it's adding layers overtop of the original
    */

    Loader {
        id: loader
        function show(component) {
            source = component;
        }
        function hide() {
            sourceComponenet = undefined;
        }
    }
}