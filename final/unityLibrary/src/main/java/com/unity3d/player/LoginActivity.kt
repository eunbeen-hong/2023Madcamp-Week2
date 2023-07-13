package com.unity3d.player

import android.content.Intent
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.util.Log
import android.widget.Toast
import com.unity3d.player.IdClass
import com.unity3d.player.RetrofitService
import com.unity3d.player.UserClass
import com.unity3d.player.databinding.ActivityLoginBinding
import retrofit2.Call
import retrofit2.Callback
import retrofit2.Response
import retrofit2.Retrofit
import retrofit2.converter.gson.GsonConverterFactory

class LoginActivity : AppCompatActivity() {
    lateinit var binding: ActivityLoginBinding
    lateinit var urlString: String
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        binding = ActivityLoginBinding.inflate(layoutInflater)
        setContentView(binding.root)

        supportActionBar?.hide()

        val id = intent.getStringExtra("id")
        val nickname = intent.getStringExtra("username")
        urlString = intent.getStringExtra("url")!!

        binding.loginWelcome.setText("환영합니다, ${nickname}님, 학교 정보를 입력해주세요!")

        binding.loginComplete.setOnClickListener {
            val retrofit = Retrofit.Builder().baseUrl(urlString)
                .addConverterFactory(GsonConverterFactory.create()).build()
            val service = retrofit.create(RetrofitService::class.java)

            val univ = binding.loginUniv.selectedItem.toString()

            var newUser = UserClass(id!!, nickname!!, univ, 0)

            service.insertUser(newUser)

            service.insertUser(newUser)?.enqueue(object :
                Callback<IdClass> {
                override fun onResponse(call: Call<IdClass>, response: Response<IdClass>) {
                    if(response.isSuccessful) {     // 생성생성
                        Toast.makeText(applicationContext, "${univ} ${nickname}유저를 생성했습니다", Toast.LENGTH_SHORT).show()

                        val intent = Intent(applicationContext, UnityPlayerActivity::class.java)
                        intent.putExtra("id", id)
                        intent.putExtra("url", urlString)
                        startActivity(intent.addFlags(Intent.FLAG_ACTIVITY_CLEAR_TOP))
                        finish()

                    } else {                        // UNEXPECTED ERROR
                        Toast.makeText(applicationContext, "서버 에러: 사용자 추가에 실패했습니다", Toast.LENGTH_SHORT).show()
                        finish()
                    }
                }

                override fun onFailure(call: Call<IdClass>, t: Throwable) {
                    Toast.makeText(applicationContext, "Sever Not Opened", Toast.LENGTH_SHORT).show()
                    Log.d("YMC", "onFailure 에러: " + t.message.toString())
                }
            })
        }

    }
}