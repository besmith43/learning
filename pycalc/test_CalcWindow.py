#!/usr/bin/env python3

import unittest
import sys
from PyQt5.QtWidgets import QDialog, QApplication, QMainWindow
from PyQt5.QtTest import QTest
from PyQt5.QtCore import Qt
from main import CalcWindow

app = QApplication(sys.argv)

class TestCalcWindow(unittest.TestCase):

    def setUp(self):
        self.w = CalcWindow()

    def test_keypad(self):
        QTest.mouseClick(self.w.ui.pushButton_0,Qt.LeftButton)
        QTest.mouseClick(self.w.ui.pushButton_1,Qt.LeftButton)
        QTest.mouseClick(self.w.ui.pushButton_2,Qt.LeftButton)
        QTest.mouseClick(self.w.ui.pushButton_3,Qt.LeftButton)
        QTest.mouseClick(self.w.ui.pushButton_4,Qt.LeftButton)
        QTest.mouseClick(self.w.ui.pushButton_5,Qt.LeftButton)
        QTest.mouseClick(self.w.ui.pushButton_6,Qt.LeftButton)
        QTest.mouseClick(self.w.ui.pushButton_7,Qt.LeftButton)
        QTest.mouseClick(self.w.ui.pushButton_8,Qt.LeftButton)
        QTest.mouseClick(self.w.ui.pushButton_9,Qt.LeftButton)
        self.assertEqual(self.w.equationText, "0123456789")

    def test_addition(self):
        # 1 + 2 = 3
        QTest.mouseClick(self.w.ui.pushButton_1,Qt.LeftButton)
        QTest.mouseClick(self.w.ui.pushButton_addition,Qt.LeftButton)
        QTest.mouseClick(self.w.ui.pushButton_2,Qt.LeftButton)
        QTest.mouseClick(self.w.ui.pushButton_equal,Qt.LeftButton)
        self.assertEqual(self.w.answer, 3)

        # 5 + -8 = -3
        QTest.mouseClick(self.w.ui.pushButton_5,Qt.LeftButton)
        QTest.mouseClick(self.w.ui.pushButton_addition,Qt.LeftButton)
        QTest.mouseClick(self.w.ui.pushButton_pos_neg,Qt.LeftButton)
        QTest.mouseClick(self.w.ui.pushButton_8,Qt.LeftButton)
        QTest.mouseClick(self.w.ui.pushButton_equal,Qt.LeftButton)
        self.assertEqual(self.w.answer, -3)

        # -5 + -8 = -13
        QTest.mouseClick(self.w.ui.pushButton_pos_neg,Qt.LeftButton)
        QTest.mouseClick(self.w.ui.pushButton_5,Qt.LeftButton)
        QTest.mouseClick(self.w.ui.pushButton_addition,Qt.LeftButton)
        QTest.mouseClick(self.w.ui.pushButton_pos_neg,Qt.LeftButton)
        QTest.mouseClick(self.w.ui.pushButton_8,Qt.LeftButton)
        QTest.mouseClick(self.w.ui.pushButton_equal,Qt.LeftButton)
        self.assertEqual(self.w.answer, -13)

    def test_subtraction(self):
        # 5 - 2 = 3
        QTest.mouseClick(self.w.ui.pushButton_5,Qt.LeftButton)
        QTest.mouseClick(self.w.ui.pushButton_subtraction,Qt.LeftButton)
        QTest.mouseClick(self.w.ui.pushButton_2,Qt.LeftButton)
        QTest.mouseClick(self.w.ui.pushButton_equal,Qt.LeftButton)
        self.assertEqual(self.w.answer, 3)

        # 5 - -8 = 13
        # currently failing with an answer of -3
        QTest.mouseClick(self.w.ui.pushButton_5,Qt.LeftButton)
        QTest.mouseClick(self.w.ui.pushButton_subtraction,Qt.LeftButton)
        QTest.mouseClick(self.w.ui.pushButton_pos_neg,Qt.LeftButton)
        QTest.mouseClick(self.w.ui.pushButton_8,Qt.LeftButton)
        QTest.mouseClick(self.w.ui.pushButton_equal,Qt.LeftButton)
        self.assertEqual(self.w.answer, 13)

        # -5 - -8 = 3
        QTest.mouseClick(self.w.ui.pushButton_pos_neg,Qt.LeftButton)
        QTest.mouseClick(self.w.ui.pushButton_5,Qt.LeftButton)
        QTest.mouseClick(self.w.ui.pushButton_subtraction,Qt.LeftButton)
        QTest.mouseClick(self.w.ui.pushButton_pos_neg,Qt.LeftButton)
        QTest.mouseClick(self.w.ui.pushButton_8,Qt.LeftButton)
        QTest.mouseClick(self.w.ui.pushButton_equal,Qt.LeftButton)
        self.assertEqual(self.w.answer, 3)

    def test_multiplication(self):
        # 1 * 2 = 2
        QTest.mouseClick(self.w.ui.pushButton_1,Qt.LeftButton)
        QTest.mouseClick(self.w.ui.pushButton_multiply,Qt.LeftButton)
        QTest.mouseClick(self.w.ui.pushButton_2,Qt.LeftButton)
        QTest.mouseClick(self.w.ui.pushButton_equal,Qt.LeftButton)
        self.assertEqual(self.w.answer, 2)

        # 5 * -8 = -40
        QTest.mouseClick(self.w.ui.pushButton_5,Qt.LeftButton)
        QTest.mouseClick(self.w.ui.pushButton_multiply,Qt.LeftButton)
        QTest.mouseClick(self.w.ui.pushButton_pos_neg,Qt.LeftButton)
        QTest.mouseClick(self.w.ui.pushButton_8,Qt.LeftButton)
        QTest.mouseClick(self.w.ui.pushButton_equal,Qt.LeftButton)
        self.assertEqual(self.w.answer, -40)

        # -5 * -8 = 40
        QTest.mouseClick(self.w.ui.pushButton_pos_neg,Qt.LeftButton)
        QTest.mouseClick(self.w.ui.pushButton_5,Qt.LeftButton)
        QTest.mouseClick(self.w.ui.pushButton_multiply,Qt.LeftButton)
        QTest.mouseClick(self.w.ui.pushButton_pos_neg,Qt.LeftButton)
        QTest.mouseClick(self.w.ui.pushButton_8,Qt.LeftButton)
        QTest.mouseClick(self.w.ui.pushButton_equal,Qt.LeftButton)
        self.assertEqual(self.w.answer, 40)

    def test_division(self):
        # 6 / 2 = 3
        QTest.mouseClick(self.w.ui.pushButton_6,Qt.LeftButton)
        QTest.mouseClick(self.w.ui.pushButton_divide,Qt.LeftButton)
        QTest.mouseClick(self.w.ui.pushButton_2,Qt.LeftButton)
        QTest.mouseClick(self.w.ui.pushButton_equal,Qt.LeftButton)
        self.assertEqual(self.w.answer, 3)

        # 40 / -8 = -5
        QTest.mouseClick(self.w.ui.pushButton_4,Qt.LeftButton)
        QTest.mouseClick(self.w.ui.pushButton_0,Qt.LeftButton)
        QTest.mouseClick(self.w.ui.pushButton_divide,Qt.LeftButton)
        QTest.mouseClick(self.w.ui.pushButton_pos_neg,Qt.LeftButton)
        QTest.mouseClick(self.w.ui.pushButton_8,Qt.LeftButton)
        QTest.mouseClick(self.w.ui.pushButton_equal,Qt.LeftButton)
        self.assertEqual(self.w.answer, -5)

        # -40 / -8 = 5
        QTest.mouseClick(self.w.ui.pushButton_pos_neg,Qt.LeftButton)
        QTest.mouseClick(self.w.ui.pushButton_4,Qt.LeftButton)
        QTest.mouseClick(self.w.ui.pushButton_0,Qt.LeftButton)
        QTest.mouseClick(self.w.ui.pushButton_divide,Qt.LeftButton)
        QTest.mouseClick(self.w.ui.pushButton_pos_neg,Qt.LeftButton)
        QTest.mouseClick(self.w.ui.pushButton_8,Qt.LeftButton)
        QTest.mouseClick(self.w.ui.pushButton_equal,Qt.LeftButton)
        self.assertEqual(self.w.answer, 5)
    
    def test_pos_neg(self):
        # *needs* check that negative sign is placed at the beginning of a number
        # *needs* check that a second negative won't get added to an already negative number
        # *needs* check that negative sign can be properly added to second number in equation
        # *needs* check that a negative sign can be removed from a number (either first number in equation or a second one)
        pass

    def test_equals(self):
        # check if the equals won't be applied if string count is zero
        QTest.mouseClick(self.w.ui.pushButton_equal,Qt.LeftButton)

        # check if the equals can be pressed safely if the string ends in an addition sign
        QTest.mouseClick(self.w.ui.pushButton_1,Qt.LeftButton)
        QTest.mouseClick(self.w.ui.pushButton_addition,Qt.LeftButton)
        QTest.mouseClick(self.w.ui.pushButton_equal,Qt.LeftButton)
        self.assertEqual(self.w.equationText, "1+")

    def test_delete(self):
        # check base case that delete will remove a single character from a string
        QTest.mouseClick(self.w.ui.pushButton_1, Qt.LeftButton)
        QTest.mouseClick(self.w.ui.pushButton_0, Qt.LeftButton)
        QTest.mouseClick(self.w.ui.pushButton_delete, Qt.LeftButton)
        self.assertEqual(self.w.equationText, "1")

        # *needs* check that delete won't error out if string is empty

    def test_clear(self):
        # check base case for clear
        QTest.mouseClick(self.w.ui.pushButton_1, Qt.LeftButton)
        QTest.mouseClick(self.w.ui.pushButton_0, Qt.LeftButton)
        QTest.mouseClick(self.w.ui.pushButton_1, Qt.LeftButton)
        QTest.mouseClick(self.w.ui.pushButton_0, Qt.LeftButton)
        QTest.mouseClick(self.w.ui.pushButton_1, Qt.LeftButton)
        QTest.mouseClick(self.w.ui.pushButton_0, Qt.LeftButton)
        QTest.mouseClick(self.w.ui.pushButton_clear, Qt.LeftButton)
        self.assertEqual(self.w.equationText, "")
    
    def test_dot(self):
        # check that the dot inserts
        QTest.mouseClick(self.w.ui.pushButton_0, Qt.LeftButton)
        QTest.mouseClick(self.w.ui.pushButton_dot, Qt.LeftButton)
        QTest.mouseClick(self.w.ui.pushButton_1, Qt.LeftButton)
        self.assertEqual(self.w.equationText, "0.1")

        # clear the string between tests
        QTest.mouseClick(self.w.ui.pushButton_clear, Qt.LeftButton)

        # check that the dot will add a zero if the equation string is of length zero
        QTest.mouseClick(self.w.ui.pushButton_dot, Qt.LeftButton)
        self.assertEqual(self.w.equationText, "0.")

        # clear the string between tests
        QTest.mouseClick(self.w.ui.pushButton_clear, Qt.LeftButton)

        # check that the dot won't apply a second time
        QTest.mouseClick(self.w.ui.pushButton_dot, Qt.LeftButton)
        QTest.mouseClick(self.w.ui.pushButton_dot, Qt.LeftButton)
        self.assertEqual(self.w.equationText, "0.")

        # clear the string between tests
        QTest.mouseClick(self.w.ui.pushButton_clear, Qt.LeftButton)

        # check that the dot won't apply when there is already a dot in the current number
        QTest.mouseClick(self.w.ui.pushButton_dot, Qt.LeftButton)
        QTest.mouseClick(self.w.ui.pushButton_1, Qt.LeftButton)
        QTest.mouseClick(self.w.ui.pushButton_dot, Qt.LeftButton)
        self.assertEqual(self.w.equationText, "0.1")

    def test_floats(self):
        # check that floating point subtraction is working
        QTest.mouseClick(self.w.ui.pushButton_3, Qt.LeftButton)
        QTest.mouseClick(self.w.ui.pushButton_dot, Qt.LeftButton)
        QTest.mouseClick(self.w.ui.pushButton_3, Qt.LeftButton)
        QTest.mouseClick(self.w.ui.pushButton_subtraction, Qt.LeftButton)
        QTest.mouseClick(self.w.ui.pushButton_2, Qt.LeftButton)
        QTest.mouseClick(self.w.ui.pushButton_dot, Qt.LeftButton)
        QTest.mouseClick(self.w.ui.pushButton_6, Qt.LeftButton)
        QTest.mouseClick(self.w.ui.pushButton_equal, Qt.LeftButton)
        self.assertEqual(self.w.answer, 0.7)


if __name__ == '__main__':
    unittest.main()
