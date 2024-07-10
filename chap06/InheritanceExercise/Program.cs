using InheritanceExercise;

Rectangle r = new(height: 3, width: 4.5);
WriteLine($"Rectangle Height: {r.Height}, Width: {r.Width}, Area: {r.Area()}");

Square s = new(5);
WriteLine($"Square Height: {s.Height}, Width: {s.Width}, Area: {s.Area()}");

Circle c = new(radius: 2.5);
WriteLine($"Circle Height: {c.Height}, Width: {c.Width}, Area: {c.Area()}");