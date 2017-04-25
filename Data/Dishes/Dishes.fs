namespace Dishes

open Types

[<AutoOpen>]
module Dishes =

    let mutable private dishes': Dish list = []

    let addDish d =
        dishes' <- d :: dishes'

    let dishes() : Dish seq =
        dishes' :> Dish seq