#!/usr/bin/env python3

from PyQt5.QtWidgets import QDialog, QApplication, QMainWindow
from pycalc import *

class CalcWindow(QMainWindow):
    equationText = ""
    answer = 0

    def __init__(self):
        super().__init__()
        self.ui = Ui_MainWindow()
        self.ui.setupUi(self)
        self.ui.actionExit.triggered.connect(self.close)
        self.ui.equation.setText(self.equationText)
        self.ui.pushButton_0.clicked.connect(self.pressedZero)
        self.ui.pushButton_1.clicked.connect(self.pressedOne)
        self.ui.pushButton_2.clicked.connect(self.pressedTwo)
        self.ui.pushButton_3.clicked.connect(self.pressedThree)
        self.ui.pushButton_4.clicked.connect(self.pressedFour)
        self.ui.pushButton_5.clicked.connect(self.pressedFive)
        self.ui.pushButton_6.clicked.connect(self.pressedSix)
        self.ui.pushButton_7.clicked.connect(self.pressedSeven)
        self.ui.pushButton_8.clicked.connect(self.pressedEight)
        self.ui.pushButton_9.clicked.connect(self.pressedNine)
        self.ui.pushButton_addition.clicked.connect(self.pressedAddition)
        self.ui.pushButton_subtraction.clicked.connect(self.pressedSubtraction)
        self.ui.pushButton_multiply.clicked.connect(self.pressedMultiplication)
        self.ui.pushButton_divide.clicked.connect(self.pressedDivision)
        self.ui.pushButton_dot.clicked.connect(self.pressedDot)
        self.ui.pushButton_pos_neg.clicked.connect(self.pressed_pos_neg)
        self.ui.pushButton_clear.clicked.connect(self.pressedClear)
        self.ui.pushButton_delete.clicked.connect(self.pressedDelete)
        self.ui.pushButton_equal.clicked.connect(self.pressedEquals)
        self.show()

    def pressedZero(self):
        self.equationText += "0"
        self.ui.equation.setText(self.equationText)

    def pressedOne(self):
        self.equationText += "1"
        self.ui.equation.setText(self.equationText)

    def pressedTwo(self):
        self.equationText += "2"
        self.ui.equation.setText(self.equationText)

    def pressedThree(self):
        self.equationText += "3"
        self.ui.equation.setText(self.equationText)

    def pressedFour(self):
        self.equationText += "4"
        self.ui.equation.setText(self.equationText)

    def pressedFive(self):
        self.equationText += "5"
        self.ui.equation.setText(self.equationText)

    def pressedSix(self):
        self.equationText += "6"
        self.ui.equation.setText(self.equationText)

    def pressedSeven(self):
        self.equationText += "7"
        self.ui.equation.setText(self.equationText)

    def pressedEight(self):
        self.equationText += "8"
        self.ui.equation.setText(self.equationText)

    def pressedNine(self):
        self.equationText += "9"
        self.ui.equation.setText(self.equationText)

    def pressedAddition(self):
        if (len(self.equationText) != 0) and (self.equationText[-1] != "+" and self.equationText[-1] != "-" and self.equationText[-1] != "*" and self.equationText[-1] != "/"):
            self.equationText += "+"
            self.ui.equation.setText(self.equationText)

    def pressedSubtraction(self):
        if (len(self.equationText) != 0) and (self.equationText[-1] != "+" and self.equationText[-1] != "-" and self.equationText[-1] != "*" and self.equationText[-1] != "/"):
            self.equationText += "-"
            self.ui.equation.setText(self.equationText)

    def pressedMultiplication(self):
        if (len(self.equationText) != 0) and (self.equationText[-1] != "+" and self.equationText[-1] != "-" and self.equationText[-1] != "*" and self.equationText[-1] != "/"):
            self.equationText += "*"
            self.ui.equation.setText(self.equationText)

    def pressedDivision(self):
        if (len(self.equationText) != 0) and (self.equationText[-1] != "+" and self.equationText[-1] != "-" and self.equationText[-1] != "*" and self.equationText[-1] != "/"):
            self.equationText += "/"
            self.ui.equation.setText(self.equationText)

    def pressedEquals(self):
        if (len(self.equationText) != 0) and (self.equationText[-1] != "+" and self.equationText[-1] != "-" and self.equationText[-1] != "*" and self.equationText[-1] != "/"):
            # switch statement to count the number of "." in the equation
            if self.equationText.count(".") >= 1:
                dot_indexes = []
                operation_indexes = []
                for i in range(0,len(self.equationText)-1):
                    if self.equationText[i] == ".":
                        dot_indexes.append(i)
                    elif self.equationText[i] == "+" or self.equationText[i] == "-" or self.equationText[i] == "*" or self.equationText[i] == "/":
                        operation_indexes.append(i)

                max_difference = 0
                for i in range(0, len(dot_indexes)-1):
                    if i == (len(dot_indexes)-1) and len(dot_indexes) > len(operation_indexes):
                        # last dot index needs to be compared against the total number of chars in the equation text
                        diff = len(self.equationText) - dot_indexes[i] - 1
                        if diff > max_difference:
                            max_difference = diff
                    elif dot_indexes[i] < operation_indexes[i]:
                        diff = operation_indexes[i] - dot_indexes[i]
                        if diff > max_difference:
                            max_difference = diff
                    else:
                        diff = dot_indexes[i] - operation_indexes[i]
                        if diff > max_difference:
                            max_difference = diff
                
                import math
                ans = round(eval(self.equationText), max_difference)
            else:
                ans = eval(self.equationText)

            self.equationText = str(ans)
            self.answer = ans
            self.ui.equation.setText(self.equationText)
            self.equationText = ""

    def pressedDot(self):
        if (len(self.equationText) == 0) or (self.equationText[-1] == "+" or self.equationText[-1] == "-" or self.equationText[-1] == "*" or self.equationText[-1] == "/"):
            self.equationText += "0."
            self.ui.equation.setText(self.equationText)
        elif self.equationText[-1] != ".":
            self.equationText += "."
            self.ui.equation.setText(self.equationText)
        else:
            if self.equationText.rfind("+") != -1 or self.equationText.rfind("-") != -1 or self.equationText.rfind("*") != -1 or self.equationText.rfind("/") != -1:
                dot_index = self.equationText.rfind(".")
                addition_index = self.equationText.rfind("+")
                subtraction_index = self.equationText.rfind("-")
                multiplication_index = self.equationText.rfind("*")
                division_index = self.equationText.rfind("/")

                if addition_index != -1 and dot_index < addition_index:
                    self.equationText += "."
                    self.ui.equation.setText(self.equationText)
                elif subtration_index != -1 and dot_index < subtraction_index:
                    self.equationText += "."
                    self.ui.equation.setText(self.equationText)
                elif multiplication_index != -1 and dot_index < multiplication_index:
                    self.equationText += "."
                    self.ui.equation.setText(self.equationText)
                elif division_index != -1 and dot_index < division_index:
                    self.equationText += "."
                    self.ui.equation.setText(self.equationText)

            else:
                self.equationText += "."
                self.ui.equation.setText(self.equationText)

    def pressed_pos_neg(self):
        # needs to be able to toggle the current number from positive to negative
        # check if first number in equation
        if (len(self.equationText) == 0) or (self.equationText[-1] != "+" and self.equationText[-1] != "-" and self.equationText[-1] != "*" and self.equationText[-1] != "/"):
            self.equationText = "-" + self.equationText
            self.ui.equation.setText(self.equationText)
        # check if a secondary number in equation
        elif (self.equationText[-1] == "+" or self.equationText[-1] == "-" or self.equationText[-1] == "*" or self.equationText[-1] == "/"):
            if self.equationText[-1] != "-":
                self.equationText += "-"
                self.ui.equation.setText(self.equationText)

    def pressedDelete(self):
        if len(self.equationText) != 0:
            self.equationText = self.equationText[:-1]
            self.ui.equation.setText(self.equationText)

    def pressedClear(self):
        self.equationText = ""
        self.answer = 0
        self.ui.equation.setText(self.equationText)


if __name__ == "__main__":
    import sys

    app = QApplication(sys.argv)

    """
    # this section is for testing the blank .ui file after conversion to a .py file
    MainWindow = QtWidgets.QMainWindow()
    ui = Ui_MainWindow()
    ui.setupUi(MainWindow)
    MainWindow.show()
    """

    w = CalcWindow()
    w.show()
    sys.exit(app.exec_())

