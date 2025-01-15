# API

The language has two basic types, which are numbers and text. Similarly, the language is also divided into these two types, meaning text does not work with numbers.

## Number

### 1. Add a number to memory

The memory is divided into 3 files, which can only be accessed by the main directory of the compiler. This was done to facilitate the analysis of the code and its results.

These files are:
<br>
<code>cache.rom</code>
<code>text_cache.rom</code>
<code>block_cache.rom</code>

1. To add numbers to memory, we use this code:

   PUT 10
