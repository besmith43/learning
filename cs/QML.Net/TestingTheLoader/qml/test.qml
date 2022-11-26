import QtQuick 2.9
import QtQuick.Controls 2.1

Item {

	Column {
		spacing: 40
		width: parent.width

		Text {
			text: "This is a test of the button"
		}

		Button {
    	    x: 50
    	    y: 450
    	    text: "Go back to Main"
    	    onClicked: {
                loader.source = ""
                loader.sourceComponent = homebutton
            }
    	}
	}
}

