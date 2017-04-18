namespace Products

open Types
open Operators

[<AutoOpen>]
module GlobalLiquids =
    let вода' = Whole "вода"
    let вода lt = вода' $ Liters lt

    let ``подсолнечное масло'``= Whole "подсолнечное масло"
    let ``подсолнечное масло`` tableSpoons = ``подсолнечное масло'`` $ TableSpoons tableSpoons

    let пиво' = Whole "пиво"
    let пиво lt = пиво' $ Liters lt
