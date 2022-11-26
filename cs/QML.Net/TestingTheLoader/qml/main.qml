import QtQuick 2.9
import QtQuick.Layouts 1.3
import QtQuick.Controls 2.3
import QtQuick.Controls.Material 2.1

ApplicationWindow {
    id: window
    width: 300
    height: 150
    visible: true
    title: "Testing The Loader"

    Component {
        id: homeButton

        Button {
            x: 50
            y: 50
            text: "Load Test Button"
            onClicked: loader.sourceComponent = testButton
        }
    }

    Component {
        id: testButton

        Button {
            x: 50
            y: 100
            text: "Unload and load to Home Button"
            onClicked: loader.sourceComponent = homeButton
        }
    }

    Component {
        id: sourceTestButton

        Button {
            x: 150
            y: 50
            text: "Load Test Page"
            onClicked: {
                loader.sourceComponent = undefined
                loader.source = "test.qml"
            }
        }
    }

    Loader {
        id: loader
        sourceComponent: homeButton
    }
}

