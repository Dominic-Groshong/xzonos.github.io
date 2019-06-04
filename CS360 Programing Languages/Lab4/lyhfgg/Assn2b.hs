{-
Author: Dominic Groshong
Version: Feb 28, 2019
Class: CS360 - Lab 4.2
-}

module Assn2b(
    domLength, rewrite1, rewrite2
) where

import Data.Char

-- #2
domLength :: [a] -> Int
domLength = foldr (\_ x -> 1 + x) 0

-- #3 TODO
--convertIntToStringLeft :: [Int] -> [Char]
--convertIntToStringRight :: [Int] -> [Char]

-- #4
rewrite1 = length $ filter(<20) [1..100]
rewrite2 = take 10 $ cycle $ filter (>5) $ map(*2) [1..100]
