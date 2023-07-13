package com.unity3d.player

import retrofit2.Call
import retrofit2.http.Body
import retrofit2.http.GET
import retrofit2.http.POST
import retrofit2.http.Path

interface RetrofitService {
    @GET("/")
    fun getAll(): Call<Array<UserClass>>

    @POST("/user/id")
    fun getUserById(@Body req: IdClass): Call<UserClass>

    @POST("user/insert")
    fun insertUser(@Body req: UserClass): Call<IdClass>

}