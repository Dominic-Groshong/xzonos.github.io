{-
Author: Dominic Groshong
Version: Feb 28, 2019
Class: CS360 - Lab 4.2
-}

-- Import Statements
import Data.List
import System.IO
import Data.Char
import Assn2b
-- Run at compile
main = do
    -- #2
    print( [fibonacci n | n <- [0..20]] )
    -- Returns [0,1,1,2,3,5,8,13,21,34,55,89,144,233,377,610,987,1597,2584,4181,6765]

    -- #4
    print (sanitize "http://wou.edu/my homepage/I love spaces.html")
    -- Returns "http://wou.edu/my%20homepage/I%20love%20spaces.html"

    -- #5
    print (multiplyListElement [1,2,3,4,5,6,7,8,9,10] 10)
    -- Returns [10,20,30,40,50,60,70,80,90,100]

    -- #6
    print (successorList "Hello")
    -- Returns "Ifmmp"

    -- #7
    print (checkList [42, 99, 15, 7, 245, 1235])
    -- Returns True
    print (checkList [40, 10, 5, 1, 245, 1235])
    -- Returns False

    -- #8
    print (listPower [5,3,8,2,3,6,3])
    -- Returns [100000,1000,100000000,100,1000,1000000,1000]

    -- #9
    print (stripEnd "This removes spaces from the end.           ")
    -- Returns "This removes spaces from the end."

    -- #10
    print (checkListEven [2, 4, 6, 8, 10])
    -- Returns True
    print (checkListEven [1,3,5,7,9])
    -- Returns False

    -- #11
    print (addNot ["Funny","cold","slow"])
    -- Returns ["Not Funny","Not cold","Not slow"]

    print (reverseListElement ["This","is","a","sentence"])
    -- Returns ["sihT","si","a","ecnetnes"]

    -- #13
    print (plus 4 3)
    -- Returns 7

    -- #14
    print (multiplyByFour 4)
    -- Returns 16

    -- #15
    print (getSecondElementList ["First", "Second", "Third", "Fourth"])
    -- Returns "Second"

    -- #16
    print (roundSqrt 77)
    -- Returns 9

    -- #17
    print (stringToArray "This is a sentence written in the usual way.")
    -- Returns  ["This","is","a","sentence","written","in","the","usual","way."]

    -- #18
    print (pythagorean [(3,4),(5,16),(9.4,2)])
    -- Returns [(3.0,4.0,5.0),(5.0,16.0,16.76305461424021),(9.4,2.0,9.610411021387172)]

    --Higher-order Functions II & Modules
    print (domLength [1,2,3,4,5])
    -- Returns 5
    print (rewrite1)
    -- Returns 19
    print (rewrite2)
    -- Returns [6,8,10,12,14,16,18,20,22,24]

{- Recursion -}
-- #1 TODO
-- gcdMine :: Integral a => a -> a -> a

-- #2
fibonacci :: Int -> Int
fibonacci 0 = 0
fibonacci 1 = 1
fibonacci n = fibonacci (n - 1) + fibonacci (n - 2)

-- #3 TODO
-- #4
sanitize [] = []
sanitize (x: xs)
         | x == ' ' =  '%' : '2' : '0' : sanitize xs
         | otherwise = x : sanitize xs

{- Higher-order functions I -}
-- #5
multiplyListElement xs t = map (t *) xs

-- #6
successorList xs = [succ x | x <- xs]

-- #7
listCheckHelper :: Int -> Bool
listCheckHelper x = if (x `mod` 42 == 0) then True else False

checkList xs = any (listCheckHelper) xs

-- #8
listPower xs = zipWith (\x y -> y^x ) xs (replicate (length xs) 10 )

-- #9
stripEnd xs | all isSpace xs = ""
stripEnd (x:xs) = x : stripEnd xs

-- #10
checkListEven xs = all even xs

-- #11
addNot xs = map ("Not " ++) xs

-- #12
reverseListElement xs = map (reverse) xs

-- #13
plus = \x -> \y -> x + y

-- #14
multiplyByFour = \x -> x*4

-- #15
getSecondElementList = \x -> x!!1

-- #16
roundSqrt = \x -> round (sqrt x)

-- #17
stringToArray = \x -> words x

-- #18
pythagorean = map (\(a,b) -> (a,b,(sqrt $ a^2 + b^2)))

{-  Higher-order Functions II & Modules -}
-- See Assn2b.hs document.
