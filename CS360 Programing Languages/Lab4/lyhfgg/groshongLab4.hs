{-
Author: Dominic Groshong
Version: Feb 23, 2019
Class: CS360 - Lab 4.1
-}

-- Import Statements
import Data.List
import System.IO
import Data.Char (ord)

-- Run at compile
main = do
    print (squareRoot 818281336460929553769504384519009121840452831049)
    print (getASCII)
    print (checkEven 12)
    print (checkSumOfOdd 100)
    print (ignoreFirstLastGetMax [99,23,4,2,67,82,49,-40])
    print (createList)
    print (getEvenList)
    print (oddDivisibleSevenThree 1 200)
    print (countOddListElements 100 200)
    print (filterPositive [-4, 6, 7, 8, -14])
    print (zipValues)
    print (makeList 5)
    print (sanitize "http://wou.edu/my homepage/I love spaces.html")
    print (getSuit 2)
    -- print (dotProduct (1,2,3.0) (4.0,5,6))
    print (feelsLike (-45.3))
    print (feelsLike2 100)
    print (feelsLike2 (-1))
    -- print (cylinderToVolume [(2,5.3),(4.2,9),(1,1),(100.3,94)])
-- #1.
squareRoot x = sqrt (fromIntegral x)
-- passing in 818281336460929553769504384519009121840452831049 evaluates to 9.045890428592033e23

-- #2
getAEnum = fromEnum 'A'
beforeAEnum = getAEnum - 1
getASCII = toEnum beforeAEnum :: Char
-- results in @

-- #3
multiplyThreeAddOne x = (x * 3) + 1
checkEven x = if mod (multiplyThreeAddOne x) 2 == 0
              then True
              else False
{-
passing in 1 = True
passing in 2 = False (2*3)+1 = 7
passing in 3 = True  (3*3)+1 = 10
-}

-- #4
checkSumOfOdd t = sum([x | x <- [1..t], odd x])
-- evaluates to 2500 if 100 is input

-- #5
ignoreFirstLastGetMax xs = maximum (tail (init xs))

-- #6
createList = 6 : 19 : 41 : (-3) : []
-- evaluates to [6,19,41,-3]

-- #7
getEvenList = take 27 [x | x <- [1..60], even x]
-- evaluates to [2,4,6,8,10,12,14,16,18,20,22,24,26,28,30,32,34,36,38,40,42,44,46,48,50,52,54]

-- #8
oddList t y = [x | x <- [t..y], odd x]
devideSevenThree x = x `mod` 7  == 0 && x `mod` 3  == 0
oddDivisibleSevenThree t y = filter devideSevenThree (oddList t y)
-- evaluates to [21, 63, 105, 147, 189] if 200 is input

-- #9
devideNine x = x `mod` 9  == 0
countOddListElements t y = length ( filter devideNine (oddList t y))
-- evaluates to 5 when 100 and 200 are passed into list.

-- #10
filterPositive xs = filter (<0) xs
-- evaluates to [-4, -14] when input is [-4, 6, 7, 8, -14]

-- #11
intValue = [1..15]
hexValue = ['1'..'9'] ++ ['A'..'F']
zipValues = zip intValue hexValue

-- #12
makeList x = take x [[y..z] | y <- [1..x], z <- [1..x]]

-- #13
sanitize xs = [x | t <- xs, x <- if(t == ' ') then "%20" else [t]]
{-
"http://wou.edu/my homepage/I love spaces.html"
evaluates as:
"http://wou.edu/my%20homepage/I%20love%20spaces.html"
-}

-- #14
{-
take 	:: Int -> [a] -> [a]
    Int is a Bounded a, or a Enum a which can become a Int, Integer, Float, or Double

head 	:: [a] -> a
    Gets the first element of a list element and output it depending on element type

length	:: Foldable t => t a -> Int
    Foldable t are a class of data structures that can be folded to a summary value.
    Int is a Bounded a, or a Enum a which can become a Int, Integer, Float, or Double

null	:: Foldable t => t a -> Bool
    Foldable t are a class of data structures that can be folded to a summary value.
    Bool outputs true or false

div		:: Integral a => a -> a -> a
    Integral a is a Int or Integer and comes from Real a
-}

-- #15
getSuit :: Int -> String
getSuit x | x == 0 = "Heart"
          | x == 1 = "Diamond"
          | x == 2 = "Spade"
          | x == 3 = "Club"
          | otherwise = "Error"
-- Returns the values above or if is not defined return "error"

-- #16 TODO
--dotProduct :: (Double,Double,Double) -> (Double,Double,Double) -> Double

-- #17 TODO
--reverseFirstThree :: [a] -> [a]


-- #18
feelsLike :: Double -> String
feelsLike degrees
    | degrees <= (-45.3) = "Frostbite Central!"
    | degrees <= 0 = "Damn Cold"
    | degrees <= 45 = "Cold"
    | degrees <= 60 = "Hot"
    | otherwise = "Like an oven out here"

-- #19
--Celsius to Farenheit
convertCF :: Double -> Double
convertCF = (+32) . (*(9/5))

feelsLike2 :: Double -> (Double, String)
feelsLike2 degrees
    | degrees <= (-45.3) = ( convertCF degrees , "Frostbite Central!")
    | degrees <= (-1)    = (convertCF degrees, "Freezing")
    | degrees <= 10      = (convertCF degrees, "Chilly")
    | degrees <= 20      = (convertCF degrees, "Just Right")
    | degrees <= 30      = (convertCF degrees, "Hot")
    | degrees <= 40      = (convertCF degrees, "Bloody Hot")
    | degrees <= 100     = (convertCF degrees, "Oven-Like")
    | otherwise          = (convertCF degrees, "Dead")

-- #20 TODO
-- cylinderToVolume :: [(Double,Double)] -> [Double]
