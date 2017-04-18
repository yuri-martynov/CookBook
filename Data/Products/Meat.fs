namespace Products

open Types
open Operators

[<AutoOpen>]
module GlobalMeat =
    let мясо' = Whole "мясо"
    let мясо grams = мясо' $ Grams grams

    let печень' = Whole "печень"
    let печень grams = печень' $ Grams grams

    let баранина' = Whole "баранина"
    let баранина grams = баранина' $ Grams grams