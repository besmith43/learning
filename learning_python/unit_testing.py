import unittest

"""
see link for more info
https://www.blog.pythonlibrary.org/2016/07/07/python-3-testing-an-intro-to-unittest/
"""

class TestPrint(unittest.TestCase):
    def test_add(self):
        self.assertEqual(2+2, 4)

if __name__ == '__main__':
    suite = unittest.TestSuite()
    result = unittest.TestResult()
    suite.addTest(unittest.makeSuite(TestPrint))
    runner = unittest.TextTestRunner()
    print(runner.run(suite))

    pikachu = """
yyyyyyyyyyyyyyyhhhhhhhhhhhhhhhyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyssoo+++/++++oooosssssyyyyyhh
yyyyyyyyyyyhhddhhhyyyyyyyyyyhhhyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyssoo+++++ooooosssssssyyyyhh
yyyyyyyyyyyyddddddsyyyyyyyyyyyhyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyssssoooooooosssssssssyyyyhh
yyyyyyyyyyyhmmdddy::/+osyyyyyyyhyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyssssssssssssssssssyyyyhh
yyyyyyyyyyyhhhddds:::::::+oyhyyhyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyhhhhyyyyyyyyyyyyyyyyyyhyydd
yyyyyyyyyyysssshdh:::::::::::+shhyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyhhhhhhhhhhhhhhhdhyso+:::dd
yyyyyyyyyyssssssyd+:::::::::::::+oyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyhhhhhhddhhdhso+/:::::::/dd
yyyyyyyyyssssssssyh/:::::::::::::::/oyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyhhhhhhhyo+:::::::::::::sdd
yyyyyyyysssssssyyyyh::::::::::::::::::+syyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyhys+/::::::::::::::::/ddd
yyyyyyyysssssssyyyyhh/::::::::::::::::::/syyyyyyyyyyyyyyyyyyyyyyyyyyyyyyso+:::::::::::::::::::::hddh
yyyyyyyssssssssyyyhhhh+:::::::::::::::::::/sssssooooo++++++++++++++ooo/::::::::::::::::::::::::ydhhh
yyyyyyysssssssyyyhhhhhhy/::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::/yhhhhh
yyyyyyyyyyyyyyyhhhhhhhhhdo::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::ohhhhhhh
yyyyhhhhhhhhhhhhhhhhhhhdddho::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::+yyyyyyyhh
yyyyhhhhhdddddddhhhhhhhdddddds//++::::::::::::::::::::::::::::::::::::::::::::::::::::::oysssssyyyhh
yyyhdddddddddddddddhhhhhddddddd+:::::::::::::::::::::::::::::::::::::::::::::::::++:/osyssssssyyyyhh
yyhddddddddddddddddddhhhhddddd+::::::::::::::::::::::::::::::::::::::::::::::::::::shyyyyyyyyyyyyyhh
yyddddddddmmmmdddddddhhhhhhhd+:::::::::+oo+::::::::::::::::::::::::/+o+/::::::::::::ohyyyyyyyyyyyhhh
yhddddmmmmmmmmmddddddhhhhhhhs:::::::::s--odh/:::::::::::::::::::::o+.:hdo::::::::::::ohyyyyyyyhhhhhh
hddddddmmmmmmmmddddddhhhhhhy:::::::::+ho+yddo:::::::::::::::::::::hs+sddh:::::::::::::yhhhhhhhhhhhhh
dddddmmmmmmmmmmmdddddhhhhhh/::::::::::yddddy::::::::::::::::::::::+hdddh+:::::::::::::/hhhhhhhhhhhhh
ddddmmmmmmmmmmmmdddddhhhhhs::::::::::::/++/:::::::::::::::::::::::::/++::::::::::::::::shhhhhhhhhhhh
ddddmmmmmmmmmmmmdddddhhhhh::::::::::::::::::::::::/hddy:::::::::::::::::::::::::::::::::hhhhhhhhhhhh
ddddmdmmmmmmmmmdddddhhhhho:/+ooooo+:::::::::::::::::::::::::::::::::::::::+ooooo+/::::::ohhhhhhhhhhh
ddddddddddddddddddhhhhhhh/+sssssssso::::::::::::::::::::::::::::::::::::/ssssssssso::::::hhhhhhhhhhh
hhhhhhhhhhhhhhhhhhhhyyyyh:osssssssss/::::::::::::::++++++++/::::::::::::ossssssssss/:::::+hyyyhhhhhh
hhhhhhhhhhhyhyyyyyyyyyyyh::+ossssso/:::::::::::::oo+///////s+::::::::::::ossssssss+:::::::yyyyyyyhhh
hhhyyyyyyyyyyyyyyyyyssssh/:::::/:::::::::::::::::y//////////y:::::::::::::/+++++/:::::::::+yyyyyyyhh
hhyyyyyysssssssssssssssssy:::::::::::::::::::::::+s////////+s::::::::::::::::::::::::::::::yssyyyyyh
hyyyyyyssssssssssssssssssyy:::::::::::::::::::::::/ooooooooo/::::::::::::::::::::::::::::::oyssyyyyh
hyyyyyysssssssssssssssssssyy::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::yssyyyyh
hyyyyyyyysssssssssssssssyyyyy:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::yyyyyyhh
hyyyyyyyyyyyyyyyyyyyyyyyyyyyyh::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::+yyyyhhh
"""

    if result.wasSuccessful():
        print(pikachu)
