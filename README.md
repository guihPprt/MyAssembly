# MyAssembly

I created this language to try to understand how language interpreters work. It is based on Assembly syntax but is not executed directly on the CPU; instead, it is run on a compiler written in C#.

A simple example of Hello World:

    T.PUT
    >Hello World<

    T.WRT 1

OUTPUT:

    Hello World
