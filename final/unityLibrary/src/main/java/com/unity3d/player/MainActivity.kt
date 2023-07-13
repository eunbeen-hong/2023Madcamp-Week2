package com.unity3d.player

import android.content.Context
import android.content.Intent
import android.content.pm.PackageManager
import android.graphics.Color
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.util.Base64
import android.util.Log
import android.view.MotionEvent
import android.view.View
import android.view.inputmethod.EditorInfo
import android.view.inputmethod.InputMethodManager
import android.widget.Toast
import com.kakao.sdk.auth.model.OAuthToken
import com.kakao.sdk.common.KakaoSdk
import com.kakao.sdk.common.model.AuthErrorCause
import com.kakao.sdk.common.model.ClientError
import com.kakao.sdk.common.model.ClientErrorCause
import com.kakao.sdk.user.UserApiClient
import com.unity3d.player.databinding.ActivityMainBinding
import retrofit2.Call
import retrofit2.Callback
import retrofit2.Response
import retrofit2.Retrofit
import retrofit2.converter.gson.GsonConverterFactory
import java.lang.Exception
import java.security.MessageDigest

class MainActivity : AppCompatActivity() {
    private lateinit var binding: ActivityMainBinding
    private lateinit var urlString: String

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        binding = ActivityMainBinding.inflate(layoutInflater)
        setContentView(binding.root)

        supportActionBar?.hide()
        urlString = getString(R.string.server_url)

        try {
            val info = packageManager.getPackageInfo(packageName, PackageManager.GET_SIGNING_CERTIFICATES)
            val signatures = info.signingInfo.apkContentsSigners
            val md = MessageDigest.getInstance("SHA")
            for (signature in signatures) {
                val md: MessageDigest
                md = MessageDigest.getInstance("SHA")
                md.update(signature.toByteArray())
                val key = String(Base64.encode(md.digest(), 0))
                Log.d("Hash Key", "$key")
            }
        } catch (e: Exception) {
            Log.e("name not found", e.toString())
        }

        KakaoSdk.init(this, "3e3a173274b20868ef2ee94a5837c5bd")

        binding.mainButton.setOnClickListener {
            kakaoLogin()
//            Toast.makeText(this, "로그인 성공!", Toast.LENGTH_SHORT).show()
        }

        val hiddenEditText = binding.hiddenEditText
        val imm = getSystemService(Context.INPUT_METHOD_SERVICE) as InputMethodManager

        hiddenEditText.setOnTouchListener { _, event ->
            when (event.action) {
                MotionEvent.ACTION_DOWN -> {
                    hiddenEditText.setTextColor(Color.BLACK)
                    hiddenEditText.visibility = View.VISIBLE
                    hiddenEditText.requestFocus()
                    imm.showSoftInput(hiddenEditText, InputMethodManager.SHOW_IMPLICIT)
                }
            }
            false
        }

        hiddenEditText.setOnEditorActionListener { _, actionId, _ ->
            if (actionId == EditorInfo.IME_ACTION_DONE) {
                hiddenEditText.visibility = View.INVISIBLE
                urlString = hiddenEditText.text.toString()
                imm.hideSoftInputFromWindow(hiddenEditText.windowToken, 0)
            }
            false
        }
    }


    private fun kakaoLogin() {
        val callback: (OAuthToken?, Throwable?) -> Unit = { token, error ->
            if (error != null) {
                when {
                    error.toString() == AuthErrorCause.AccessDenied.toString() -> {
                        Toast.makeText(this, "접근이 거부 됨", Toast.LENGTH_SHORT).show()
                        Log.d("mytag", error.toString())
                    }
                    error.toString() == AuthErrorCause.InvalidClient.toString() -> {
                        Toast.makeText(this, "유효하지 않은 앱", Toast.LENGTH_SHORT).show()
                        Log.d("mytag", error.toString())
                    }
                    error.toString() == AuthErrorCause.InvalidGrant.toString() -> {
                        Toast.makeText(this, "인증 수단이 유효하지 않음", Toast.LENGTH_SHORT).show()
                        Log.d("mytag", error.toString())
                    }
                    error.toString() == AuthErrorCause.InvalidRequest.toString() -> {
                        Toast.makeText(this, "요청 파라미터 오류", Toast.LENGTH_SHORT).show()
                        Log.d("mytag", error.toString())
                    }
                    error.toString() == AuthErrorCause.InvalidScope.toString() -> {
                        Toast.makeText(this, "유효하지 않은 Scope ID", Toast.LENGTH_SHORT).show()
                        Log.d("mytag", error.toString())
                    }
                    error.toString() == AuthErrorCause.Unauthorized.toString() -> {
                        Toast.makeText(this, "앱에 요청 권한이 없음", Toast.LENGTH_SHORT).show()
                        Log.d("mytag", error.toString())
                    }
                    else -> {
                        Toast.makeText(this, "기타 에러", Toast.LENGTH_SHORT).show()
                        Log.d("mytag", error.toString())
                        //AuthError(statusCode=401, reason=Misconfigured, response=AuthErrorResponse(error=misconfigured, errorDescription=invalid android_key_hash or ios_bundle_id or web_site_url))
                    }
                }
            } else if (token != null) {
                Toast.makeText(this, "로그인 성공!", Toast.LENGTH_SHORT).show()
                UserApiClient.instance.me { user, error ->
                    if (error != null) {
//                            intent.putExtra("username", "USERNAME_ERROR")
                    } else if (user != null) {
                        val retrofit = Retrofit.Builder().baseUrl(urlString)
                            .addConverterFactory(GsonConverterFactory.create()).build()
                        val service = retrofit.create(RetrofitService::class.java)

                        Log.e("URLSTRING", urlString)

                        service.getUserById(IdClass(user.id.toString()))?.enqueue(object :
                            Callback<UserClass> {
                            override fun onResponse(call: Call<UserClass>, response: Response<UserClass>) {
                                if(response.isSuccessful) {     // 유저가 이미 있으면 -> StartActivity
                                    var result: UserClass? = response.body()
                                    Toast.makeText(applicationContext, "${result!!.univ} ${result!!.username}으로 로그인 합니다", Toast.LENGTH_SHORT).show()

//                                    val intent = Intent(applicationContext, StartActivity::class.java)
//                                    intent.putExtra("id", user.id.toString())
//                                    intent.putExtra("username", user.kakaoAccount?.profile?.nickname)
//                                    startActivity(intent.addFlags(Intent.FLAG_ACTIVITY_CLEAR_TOP))

                                    val intent = Intent(applicationContext, UnityPlayerActivity::class.java)
                                    intent.putExtra("id", user.id.toString())
                                    intent.putExtra("url", urlString)
//                                    intent.putExtra("username", user.kakaoAccount?.profile?.nickname)
                                    startActivity(intent.addFlags(Intent.FLAG_ACTIVITY_CLEAR_TOP))
//                                    finish()

                                } else {                        // UNEXPECTED ERROR
                                    Toast.makeText(applicationContext, "UNEXPECTED ERROR", Toast.LENGTH_SHORT).show()
                                }
                            }

                            override fun onFailure(call: Call<UserClass>, t: Throwable) {
                                Toast.makeText(applicationContext, "새로운 유저를 생성합니다", Toast.LENGTH_SHORT).show()

                                val intent = Intent(applicationContext, LoginActivity::class.java)
                                intent.putExtra("id", user.id.toString())
                                intent.putExtra("url", urlString)
                                intent.putExtra("username", user.kakaoAccount?.profile?.nickname)
                                startActivity(intent.addFlags(Intent.FLAG_ACTIVITY_CLEAR_TOP))
                                finish()
                            }
                        })

                    }
                }
            }
        }

        if (UserApiClient.instance.isKakaoTalkLoginAvailable(this)) {
            UserApiClient.instance.loginWithKakaoTalk(this) { token, error ->
                if (error != null) {
                    Toast.makeText(this, "토큰 정보 보기 실패", Toast.LENGTH_SHORT).show()

                    if (error is ClientError && error.reason == ClientErrorCause.Cancelled) {
                        return@loginWithKakaoTalk
                    }

                    UserApiClient.instance.loginWithKakaoAccount(this, callback = callback)
                } else if (token != null) {
                    Toast.makeText(this, "토큰 정보 보기 성공", Toast.LENGTH_SHORT).show()
                    UserApiClient.instance.me { user, error ->
                        if (error != null) {
//                            intent.putExtra("username", "USERNAME_ERROR")
                        } else if (user != null) {
                            val retrofit = Retrofit.Builder().baseUrl(urlString)
                                .addConverterFactory(GsonConverterFactory.create()).build()
                            val service = retrofit.create(RetrofitService::class.java)

                            service.getUserById(IdClass(user.id.toString()))?.enqueue(object :
                                Callback<UserClass> {
                                override fun onResponse(call: Call<UserClass>, response: Response<UserClass>) {
                                    if(response.isSuccessful) {     // 유저가 이미 있으면 -> StartActivity
                                        var result: UserClass? = response.body()
                                        Toast.makeText(applicationContext, "${result!!.univ} ${result!!.username}으로 로그인 합니다", Toast.LENGTH_SHORT).show()

                                        val intent = Intent(applicationContext, UnityPlayerActivity::class.java)
                                        intent.putExtra("id", user.id.toString())
                                        intent.putExtra("url", urlString)
//                                        Log.e("HERE", "${user.id.toString()}")
//                                        intent.putExtra("username", user.kakaoAccount?.profile?.nickname)
                                        startActivity(intent.addFlags(Intent.FLAG_ACTIVITY_CLEAR_TOP))
                                        finish()

                                    } else {                        // UNEXPECTED ERROR
                                        Toast.makeText(applicationContext, "Server Not Opened", Toast.LENGTH_SHORT).show()
                                    }
                                }

                                override fun onFailure(call: Call<UserClass>, t: Throwable) {
                                    Toast.makeText(applicationContext, "새로운 유저를 생성합니다", Toast.LENGTH_SHORT).show()

                                    val intent = Intent(applicationContext, LoginActivity::class.java)
                                    intent.putExtra("id", user.id.toString())
                                    intent.putExtra("url", urlString)
                                    intent.putExtra("username", user.kakaoAccount?.profile?.nickname)
                                    startActivity(intent.addFlags(Intent.FLAG_ACTIVITY_CLEAR_TOP))
                                    finish()
                                }
                            })

                        }
                    }
                }
            }
        } else {
            UserApiClient.instance.loginWithKakaoAccount(this, callback = callback)
        }
    }
}