from matplotlib.backends.backend_pdf import PdfPages
import matplotlib.pyplot as plt
from scipy.misc import imread
import os
import numpy as np

files = [ "test1.jpg", "test2.jpg" ]
def plotImage(f):
    folder = "C:/temp/"
    im = imread(os.path.join(folder, f)).astype(np.float32) / 255
    plt.imshow(im)
    a = plt.gca()
    a.get_xaxis().set_visible(False) # We don't need axis ticks
    a.get_yaxis().set_visible(False)

pwd = os.getcwd()
pp = PdfPages(pwd + "\\page1.pdf")
plt.subplot(121)
plotImage(files[0])
plt.subplot(122)
plotImage(files[1])
pp.savefig(plt.gcf()) # This generates page 1
pp.savefig(plt.gcf()) # This generates page 2
pp.close()