16 constant width
create buf width allot
variable fd
variable pos

: .hex ( u n -- ) base @ >r hex >r 0 <# r> 0 ?do # loop #> type r> base ! ;
: .pos ( -- ) pos @ 8 .hex ." : " ;

: .bytes ( n -- )
    width 0 do
        i over < if buf i + c@ 2 .hex space else 3 spaces then
    loop drop ;

: .ascii ( n -- )
    0 ?do
        buf i + c@ dup 32 127 within 0= if drop [char] . then emit
    loop ;

: line ( n -- ) .pos dup .bytes space .ascii cr ;

: dump-file ( -- )
    begin buf width fd @ read-file throw dup while
        dup line width pos +!
    repeat drop ;

: main ( -- )
    next-arg dup 0= if 2drop ." usage: gforth hexview.fs <file>" cr bye then
    r/o open-file throw fd ! dump-file fd @ close-file throw bye ;

main
