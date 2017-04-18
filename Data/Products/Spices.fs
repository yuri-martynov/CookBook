namespace Products

open Types
open Operators

[<AutoOpen>]
module GlobalSpices =
    let соль = Whole "соль"
    let соли = соль $ ToTaste

    let перец = Whole "перец"
    let переца = перец $ ToTaste

    let ``соли и переца по вкусу`` =
        соли &&& переца

