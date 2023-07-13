package com.unity3d.player

import com.google.gson.annotations.SerializedName

data class UserClass (
    @SerializedName("id")
    val id: String,

    @SerializedName("username")
    val username: String,

    @SerializedName("univ")
    val univ: String,

    @SerializedName("bestScore")
    val bestScore: Int,
)