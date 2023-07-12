# [몰입캠프 Week 2] - OlaOla (올라올라)

APK는 [여기](https://drive.google.com/file/d/10UJZOl9N5TSMK19nRUQgTM-5uFRl5Bnc/view?usp=sharing)에서 다운로드 할 수 있습니다.

<img width="50%" alt="IMG_2486" src="https://github.com/pancakesontuesday/2023Madcamp-Week2/assets/109589438/9555cd59-48cd-4627-bc2f-880399175d9f">
<img width="50%" alt="IMG_2486" src="https://github.com/pancakesontuesday/2023Madcamp-Week2/assets/109589438/9d6d84e7-ddc4-4e30-a8eb-b750ed9a2d89">


## 개발자 (Developers)

- [홍은빈](https://github.com/pancakesontuesday), [김은수](https://github.com/EunsuKim03)
<img width="13%" alt="IMG_2489" src="https://github.com/pancakesontuesday/2023Madcamp-Week2/assets/109589438/7fd0c146-0a92-4bad-9d2c-917ae8e03b26">
<img width="10%" alt="IMG_2488" src="https://github.com/pancakesontuesday/2023Madcamp-Week2/assets/109589438/095d9439-0f6a-4703-b937-337ac28fae97">



## 기술 (Tech Stack)

[![](https://img.shields.io/badge/Unity-000000?style=for-the-badge&logo=unity&logoColor=white)](https://unity.com/)
[![](https://img.shields.io/badge/Android_Studio-3DDC84?style=for-the-badge&logo=android-studio&logoColor=white)](https://developer.android.com/studio)
[![](https://img.shields.io/badge/Node.js-339933?style=for-the-badge&logo=node.js&logoColor=white)](https://nodejs.org/)
[![](https://img.shields.io/badge/MongoDB-47A248?style=for-the-badge&logo=mongodb&logoColor=white)](https://www.mongodb.com/)


### 게임
*Unity 2044.3.4f1*

### 서버
*Node.js + Ubuntu 20.4 VM*   
* 서버 API   
  * REST 기반 API   
    * GET: /

### DB
*MongoDB Atlas*

### 카카오 로그인 & 배포
*Android Studio*


## 소개 (Introduction)
<img width="10%" alt="IMG_2487" src="https://github.com/pancakesontuesday/2023Madcamp-Week2/assets/109589438/daecbc1c-c984-43f6-a010-dd8357c25496">

`올라올라`는 무한의 계단을 모티브로 만들어진 몰입캠프 대학별 대항 게임입니다. 카카오 로그인으로 게임을 이용할 수 있으며 최초 가입시 선택한 대학으로 캐릭터가 결정됩니다. 랭킹탭에서 가입한 유저들의 순위를 확인할 수 있으며 1, 2, 3위는 대학 캐릭터와 함께 명예의 전당에 오르게 됩니다. 대학의 명예를 걸고 계단을 올라올라~

<img width="30%" alt="IMG_2487" src="https://github.com/pancakesontuesday/2023Madcamp-Week2/assets/109589438/46cfc998-9840-4342-a67f-14c6baee229f">


## 기능 (Features)

### 홈화면
 - 유저의 대학에 따라 캐릭터가 지정됩니다. 숙명여대, 한양대, 성균관대, 카이스트, 고려대, GIST, 포스텍은 대학의 마스코트가 캐릭터로 제공되고 이외의 대학은 기본 캐릭터로 플레이하게 됩니다.
 - 홈 화면에서는 랭킹탭과 게임화면에 접근할 수 있습니다.
 - 앱을 실행하면 배경음악을 들을 수 있습니다.

<img width="30%" alt="home0" src="https://github.com/pancakesontuesday/2023Madcamp-Week2/assets/109589438/9a432e01-2271-4ee4-a56c-5ea8b8fc427b">
<img width="30%" alt="home1" src="https://github.com/pancakesontuesday/2023Madcamp-Week2/assets/109589438/d57f664d-49e7-4ff4-bbcb-6b3c1e2e27ea">
<img width="30%" alt="home2" src="https://github.com/pancakesontuesday/2023Madcamp-Week2/assets/109589438/8481562e-5cea-4f7c-bd60-87002affb404">


### 게임화면
 - 오른쪽, 왼쪽 버튼으로 계단을 오를 방향을 선택합니다.
 - 상단의 타이머로 남은 시간을 확인할 수 있습니다. 타이머가 0에 도달할 때까지 계단을 오르지 않으면 게임이 오버됩니다.
 - 오른쪽 상단에서 게임을 잠시 멈추고 다시 실행할 수 있습니다.
 - 캐릭터가 죽으면 비명소리와 함께 캐릭터의 sprite가 변경됩니다.


<img width="30%" alt="game0" src="https://github.com/pancakesontuesday/2023Madcamp-Week2/assets/109589438/80893578-78f9-47d3-821f-4f3770fb4b63">
<img width="30%" alt="game1" src="https://github.com/pancakesontuesday/2023Madcamp-Week2/assets/109589438/28e22bff-e9b1-4373-bfcf-07a0c9327042">
<img width="30%" alt="home2" src="https://github.com/pancakesontuesday/2023Madcamp-Week2/assets/109589438/58b012db-7ee5-4ca9-8709-c61f4babd0f8">


### 랭킹탭
 - 랭킹탭에서는 모든 유저의 순위를 확인할 수 있습니다.
 - 상단에서는 나의 최고 점수를 확인할 수 있으며, 홈버튼으로 홈화면으로 돌아갈 수 있습니다.
 - 1, 2, 3등은 대학 마스코트와 함께 명예의 전당에 오르게 됩니다.
 - Scroll view로 다른 모든 유저의 등수, 소속대학, 유저 이름과 최고점수를 확인할 수 있습니다.

<img width="30%" alt="ranking" src="https://github.com/pancakesontuesday/2023Madcamp-Week2/assets/109589438/29bbad8b-6df1-4052-b50a-1d659817dad9">
