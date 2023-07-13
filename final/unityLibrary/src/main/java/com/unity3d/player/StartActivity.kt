package com.unity3d.player

import android.content.Intent
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.widget.Toast
import com.unity3d.player.databinding.ActivityStartBinding

class StartActivity : AppCompatActivity() {
    lateinit var binding: ActivityStartBinding
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        binding = ActivityStartBinding.inflate(layoutInflater)
        setContentView(binding.root)

        supportActionBar?.hide()

        binding.startButton.setOnClickListener {
            val id = intent.getStringExtra("id")!!
            val intent = Intent(applicationContext, UnityPlayerActivity::class.java)
            intent.putExtra("id", id)
            Toast.makeText(applicationContext, "GAME START", Toast.LENGTH_SHORT).show()
            startActivity(intent.addFlags(Intent.FLAG_ACTIVITY_CLEAR_TOP))

        }
    }
}