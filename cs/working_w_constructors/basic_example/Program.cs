
public class thingy {
	public int a { get; set; }
	public int b { get; set; }

	public int add() {
		return a + b;
	}
}

public class dojobbers {
	public int a { get; set; }
	public int b { get; set; }

	// can't have a constructor with no parameters unless you set default values
	public dojobbers() {
		a = 0;
		b = 0;
	}
	public dojobbers(int a, int b) {
		this.a = a;
		this.b = b;
	}

	public int add() {
		return a + b;
	}
}

public class Program {
	static void Main() {
		thingy t = new thingy();
		t.a = 1;
		t.b = 2;
		Console.WriteLine(t.add());

		dojobbers d = new dojobbers(1, 2);
		Console.WriteLine(d.add());

		dojobbers d2 = new dojobbers();
		Console.WriteLine(d2.add());


	}
}

