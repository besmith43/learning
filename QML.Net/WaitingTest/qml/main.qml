import QtQuick 2.9
import QtQuick.Layouts 1.3
import QtQuick.Controls 2.3
import QtQuick.Controls.Material 2.1
import WaitingTest 1.0

ApplicationWindow {
    id: window
    width: 360
    height: 520
    visible: true

    Column {
        x: 10
        y: 10
        spacing: 40
        width: parent.width

        Button {
            text: 'Change Text Value'
            onClicked: {
                processingDialog.open()
                var task = model.runTask('This is a test')
                Net.await(task, function(result) {
                    message.text = result
                    processingDialog.close()
                })
            }
        }

        Text {
            id: message
        }

        TestModel {
            id: model
        }
    }

    Dialog {
        id: processingDialog
        modal: true
        focus: true
        title: "Processing"
        x: (window.width - width) / 2
        y: window.height / 6
        width: Math.min(window.width, window.height) / 3 * 2

        BusyIndicator {
            running: true
        }
    }
}