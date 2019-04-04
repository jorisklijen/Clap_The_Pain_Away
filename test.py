from tkinter import *
import math

class Application(Frame):
    """ Main class for calculator"""

    def __init__(self, master):
        """ Initialise the Frame. """
        super(Application, self).__init__(master)
        self.task = ""
        self.UserIn = StringVar()
        self.grid()
        self.create_widgets()

    def create_widgets(self):
        """ Create all the buttons for calculator. """

        #Invoer
        self.user_input = Entry(self, bg = "white", bd = 29,
        insertwidth = 4, width = 42,
        font = ("Verdana", 20, "bold"), textvariable = self.UserIn, justify = RIGHT)
        self.user_input.grid(columnspan = 6)
        self.user_input.insert(0, "0")

    #DOET HET
        #Button voor waarde 7
        self.button1 = Button(self, bg = "peachpuff", bd = 12,
        text = "7", padx = 38, pady = 25, font = ("Helvetica", 20, "bold"), width = 4,
        command = lambda  : self.buttonClick(7))
        self.button1.grid(row = 3, column = 0, sticky = W)

    #DOET HET
        #Button voor waarde 8
        self.button2 = Button(self, bg = "peachpuff", bd = 12,
        text = "8",  padx = 38, pady = 25,
        command = lambda : self.buttonClick(8), font = ("Helvetica", 20, "bold"), width= 4)
        self.button2.grid(row = 3, column = 1, sticky = W)

    #DOET HET
        #Button voor waarde 9
        self.button3 = Button(self, bg = "peachpuff", bd = 12,
        text = "9",  padx = 38, pady = 25,
        command = lambda : self.buttonClick(9), font = ("Helvetica", 20, "bold"), width = 4)
        self.button3.grid(row = 3, column = 2, sticky = W)

    #DOET HET
        #Button voor waarde 4
        self.button4 = Button(self, bg = "peachpuff", bd = 12,
        text = "4",  padx = 38, pady = 25,
        command = lambda : self.buttonClick(4), font = ("Helvetica", 20, "bold"), width = 4)
        self.button4.grid(row = 4, column = 0, sticky = W)

    #DOET HET
        #Button voor waarde 5
        self.button5 = Button(self, bg = "peachpuff", bd = 12,
        text = "5",  padx = 38, pady = 25,
        command = lambda : self.buttonClick(5), font = ("Helvetica", 20, "bold"), width = 4)
        self.button5.grid(row = 4, column = 1, sticky = W)

    #DOET HET
        #Button voor waarde 6
        self.button6 = Button(self, bg = "peachpuff", bd = 12,
        text = "6",  padx = 38, pady = 25,
        command = lambda : self.buttonClick(6), font = ("Helvetica", 20, "bold"), width = 4)
        self.button6.grid(row = 4, column = 2, sticky = W)

    #DOET HET
        #Button voor waarde 1
        self.button7 = Button(self, bg = "peachpuff", bd = 12,
        text = "1",  padx = 38, pady = 25,
        command = lambda : self.buttonClick(1), font = ("Helvetica", 20, "bold"), width = 4)
        self.button7.grid(row = 5, column = 0, sticky = W)

    #DOET HET
        #Button voor waarde 2
        self.button8 = Button(self, bg = "peachpuff", bd = 12,
        text = "2",  padx = 38, pady = 25,
        command = lambda : self.buttonClick(2), font = ("Helvetica", 20, "bold"), width = 4)
        self.button8.grid(row = 5, column = 1, sticky = W)

    #DOET HET
        #Button voor waarde 3
        self.button9 = Button(self, bg = "peachpuff", bd = 12,
        text = "3",  padx = 38, pady = 25,
        command = lambda : self.buttonClick(3), font = ("Helvetica", 20, "bold"), width = 4)
        self.button9.grid(row = 5, column = 2, sticky = W)

    #DOET HET
        #Button voor waarde 0
        self.button9 = Button(self, bg="peachpuff", bd = 12,
        text = "0",  padx = 38, pady = 25,
        command = lambda : self.buttonClick(0), font = ("Helvetica", 20, "bold"), width = 4)
        self.button9.grid(row = 6, column = 1, sticky = W)

        #Buttons voor de functies

    #DOET HET
        #Optellen button
        self.Addbutton = Button(self, bg = "peachpuff", bd = 12,
        text = "+",  padx = 38, pady = 25,
        command = lambda : self.buttonClick("+"), font = ("Helvetica", 20, "bold"), width = 4)
        self.Addbutton.grid(row = 3, column = 3, sticky = W)

    #DOET HET
        #Aftrekken button
        self.Subbutton = Button(self, bg = "peachpuff", bd = 12,
        text = "-",  padx = 38, pady = 25,
        command = lambda : self.buttonClick("-"), font = ("Helvetica", 20, "bold"), width = 4)
        self.Subbutton.grid(row = 4, column = 3, sticky = W)

    #DOET HET
        #Vermenigvuldigen button
        self.Multbutton = Button(self, bg = "peachpuff", bd = 12,
        text = "*",  padx = 38, pady = 25,
        command = lambda : self.buttonClick("*"), font = ("Helvetica", 20, "bold"), width = 4)
        self.Multbutton.grid(row = 5, column = 3, sticky = W)

    #DOET HET
        #Delen button
        self.Divbutton = Button(self, bg = "peachpuff", bd = 12,
        text = "/",  padx = 38, pady = 25,
        command = lambda : self.buttonClick("/"), font = ("Helvetica", 20, "bold"), width = 4)
        self.Divbutton.grid(row = 6, column = 3, sticky = W)

    #DOET HET
        #=-teken button
        self.Equalbutton = Button(self, bg = "lightgrey", bd = 12,
        text = "=",  padx = 38, pady = 25,
        command = self.CalculateTask, font = ("Helvetica", 20, "bold"), width= 4)
        self.Equalbutton.grid(row = 6, column = 0, sticky = W)

    #DOET HET
        #AC button
        self.Clearbutton = Button(self, bg = "lightgrey", bd = 12,
        text = "AC", font = ("Helvetica", 20, "bold"), width = 48, padx = 8,
        command = self.ClearDisplay)
        self.Clearbutton.grid(row = 1, columnspan = 6, sticky = W)

        #Sinus button
        self.Sinusbutton = Button(self, bg = "peachpuff", bd = 12,
        text = "sin",  padx = 38, pady = 25,
        command = lambda : self.buttonClick4("sin("), font = ("Helvetica", 20, "bold"), width=4)
        self.Sinusbutton.grid(row = 2, column = 0, sticky = W)

        #Cosinus button
        self.Cosinusbutton = Button(self, bg = "peachpuff", bd = 12,
        text = "cos",  padx = 38, pady = 25,
        command = lambda : self.buttonClick("cos("), font = ("Helvetica", 20, "bold"), width = 4)
        self.Cosinusbutton.grid(row = 2, column = 1, sticky = W)

        #Tangens button
        self.Tangensbutton = Button(self, bg = "peachpuff", bd = 12,
        text = "tan",  padx = 38, pady = 25,
        command = lambda : self.buttonClick("tan("), font = ("Helvetica", 20, "bold"), width = 4)
        self.Tangensbutton.grid(row = 2, column = 2, sticky = W)

    #DOET HET
        #Punt button
        self.Puntbutton = Button(self, bg = "peachpuff", bd = 12,
        text = ".",  padx = 38, pady = 25,
        command = lambda : self.buttonClick("."), font = ("Helvetica", 20, "bold"), width = 4)
        self.Puntbutton.grid(row = 6, column = 2, sticky = W)

    #DOET HET
        #Button voor de waarde pi
        self.buttonpi = Button(self, bg="peachpuff", bd = 12,
        text = "π",  padx = 38, pady = 25,
        command = lambda : self.buttonClick2('π'), font = ("Helvetica", 20, "bold"), width = 4)
        self.buttonpi.grid(row = 3, column = 4, sticky = W)

        #Logaritme button
        self.Logbutton = Button(self, bg = "peachpuff", bd = 12,
        text = "log",  padx = 38, pady = 25,
        command = lambda : self.buttonClick("log("), font = ("Helvetica", 20, "bold"), width = 4)
        self.Logbutton.grid(row = 2, column = 3, sticky = W)

        #Natuurlijk logaritme button
        self.Lnbutton = Button(self, bg = "peachpuff", bd = 12,
        text = "ln",  padx = 38, pady = 25,
        command = lambda : self.buttonClick("ln("), font = ("Helvetica", 20, "bold"), width = 4)
        self.Lnbutton.grid(row = 2, column = 4, sticky = W)

    #DOET HET
        #Button voor de waarde e
        self.buttone = Button(self, bg="peachpuff", bd = 12,
        text = "e",  padx = 38, pady = 25,
        command = lambda : self.buttonClick3('e'), font = ("Helvetica", 20, "bold"), width = 4)
        self.buttone.grid(row = 4, column = 4, sticky = W)

    #DOET HET
        #Haakje-open button
        self.Haakjeopenbutton = Button(self, bg = "peachpuff", bd = 12,
        text = "(",  padx = 38, pady = 25,
        command = lambda : self.buttonClick("("), font = ("Helvetica", 20, "bold"), width = 4)
        self.Haakjeopenbutton.grid(row = 5, column = 4, sticky = W)

    #DOET HET
        #Haakje-sluiten
        self.Haakjesluitenbutton = Button(self, bg = "peachpuff", bd = 12,
        text = ")",  padx = 38, pady = 25,
        command = lambda : self.buttonClick(")"), font = ("Helvetica", 20, "bold"), width = 4)
        self.Haakjesluitenbutton.grid(row = 6, column = 4, sticky = W)


    def buttonClick(self, number):
        self.task = str(self.task) + str(number)
        self.UserIn.set(self.task)


    def buttonClick2(self, number):
        number = math.pi;
        self.task = str(self.task) + str(number);
        self.UserIn.set(self.task);

    def buttonClick3(self, number):
        number = math.e
        self.task = str(self.task) + str(number)
        self.UserIn.set(self.task)

    def buttonClick4(self,number):



    def CalculateTask(self):
        self.data = self.user_input.get()
        try:
            self.answer = eval(self.data)
            self.displayText(self.answer)
            self.task = self.answer

        except SyntaxError as e:
            self.displayText("Invalid Syntax!")
            self.task = ""

    def displayText(self, value):
        self.user_input.delete(0, END)
        self.user_input.insert(0, value)

    def ClearDisplay(self):
        self.task = ""
        self.user_input.delete(0, END)
        self.user_input.insert(0, "0")


calculator = Tk()

calculator.title("Calculator")
app = Application(calculator)
# Make window fixed (cannot be resized)
calculator.resizable(width = False, height = False)

calculator.mainloop()
