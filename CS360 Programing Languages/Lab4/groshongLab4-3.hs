{-
Author: Dominic Groshong
Professor: Beka Morgan
Version: Feb 23, 2019
Class: CS360 - Lab 4.1
-}

-- Import Statements
import Data.List
import System.IO
import Data.Char (ord)

-- Run at compile
main = do
    -- Example
    print (squareRoot 818281336460929553769504384519009121840452831049)
    -- Result 9.045890428592033e23

    -- 1
    print (price 62.3 78.4)
    -- Result 0.2798965238095238

    -- 2
    print (flightDistance (45.0,-122.0) (21.0,-158.0))

    -- 3
    print (sumOddCubed [1000..2000])
    -- Result 1874999250000

    -- 4.1
    print (removeSpaces "Help me please")
    -- Result "Helpmeplease"

    -- 4.2
    print (filterEven [1,2,3,4,5,6,7,8,9,10])
    -- Result [2,4,6,8,10]

    -- 4.3
    print (doubleList [1,2,3,4,5,6,7,9,10])
    -- Result [2,4,6,8,10,12,14,18,20]

    -- 4.4
    print (checkListFiftyFive [1,2,3,4,5])
    -- Result False
    print (checkListFiftyFive [1,2,3,4,55])
    -- Result True

    -- 4.5
    print (checkListOdd [1,3,5,7,9])
    -- Result True

    -- 5
    print (primeList [1000..1020])
    -- Result [1009,1013,1019]

    -- 6 TODO FIX THIS SHIT
    print (factor 175561)
    -- Result [419]
    print (factor 62451532000)
    -- Result [2,5,11,13,23,47,101]
    
-- Example.
squareRoot x = sqrt (fromIntegral x)
-- passing in 818281336460929553769504384519009121840452831049 evaluates to 9.045890428592033e23

-- #1
-- Convert CAD to USD
usd x = x * 0.75
-- Convert Liters to Gallons
gallons x = x * 0.264172
-- Get Miles Per Gallon
price x y = gallons x / usd y

-- #2
radians :: (Double,Double) -> (Double,Double)
radians (x,y) = (x * (pi/180), y * (pi/180))

flightDistance (x1,y1) (x2,y2) = 3963 * acos ((cos(a)*cos(c)*cos(b-d)) + (sin(a)*sin(b)))
                                 where a = fst (radians (x1, y1))
                                       b = snd (radians (x1,y1))
                                       c = fst (radians (x2,y2))
                                       d = snd (radians (x2,y2))

-- #3
sumOddCubed xs = sum ( [x^3 | x <- xs, odd x] )

{- #4 Write expressions using map, filter, any, or all -}
-- #4.1
removeSpaces xs = filter (/=' ') xs

-- #4.2
filterEven xs = filter(even) xs

-- #4.3
doubleList xs = map (2 *) xs

-- #4.4
checkListFiftyFive xs = any (55 ==) xs

-- #4.5
checkListOdd xs = all odd xs

-- #5
isPrime y = null [ x | x <- [2..floor (squareRoot y)], y `mod` x == 0 ]
primeList xs = filter (\x -> isPrime x) xs

-- #6
factor n = take 7 [p | p <- takeWhile (<=n) $ primeList [2..], n `mod` p == 0]
