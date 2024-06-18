/*
    Operators and operations on them.
*/
// Increment and addition operator
int x = 3;
int y = 2 + ++x;
WriteLine($"x is {x} and y is {y}\n");

// Binary shift operators
int a = 3;
int d = a << 2; // 3 * 4
byte c = (byte) a;
WriteLine($"d is {d} and c is {c}\n");

int b = 10 >> 1; // 10 / 2
WriteLine($"b is {b}\n");

// Bitwise operators
int f = 10 & 8; // 1010 & 1000 => 1000 (8)
WriteLine($"f is {f}\n");

int g = 10 | 7; // 1010 | 0111 => 1111 (15)
WriteLine($"g is {g}\n");