import QtQuick 2.9
import QtQuick.Layouts 1.3
import QtQuick.Controls 2.3
import QtQuick.Controls.Material 2.1
import QtQuick.Dialogs 1.0

ApplicationWindow {
    id: window
    width: 300
    height: 150
    visible: true
    title: "File Browser Test"

    Button {
        text: "Open File"
        onClicked: loader.source = "dialogtest.qml"
    }

    Loader {
        id: loader
    }
}