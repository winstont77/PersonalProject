<script setup>
import axios from 'axios';
import { ref, onMounted } from 'vue'
import router from "../router/router.js"

let prop = defineProps({
  odds:null,
  team:null,
  event:{}
})

let emit = defineEmits(['betResultEmit'])

let betResultButton = (display, sureOrCancel)=>{
    if(sureOrCancel===true){
        let prime;
        // sendCard()

        // 取得 TapPay Fields 的 status
        const tappayStatus = TPDirect.card.getTappayFieldsStatus()

        // 確認是否可以 getPrime
        if (tappayStatus.canGetPrime === false) {
            alert('can not get prime')
            return
        }

        // Get prime
        TPDirect.card.getPrime((result) => {
            if (result.status !== 0) {
                alert('get prime error ' + result.msg)
                return
            }
            alert('get prime 成功，prime: ' + result.card.prime)
            prime = result.card.prime;

            axios.post("https://localhost:7099/PostBets",{
                "Bet": {
                    "EventId":prop.event.id,
                    "DateTime": getCurrentDateInFormat().toString(),
                    "Money": betMoney.value,
                    "AwayTeamName":prop.event.awayTeamName,
                    "AwayTeamOdds":prop.event.awayTeamOdds,
                    "HomeTeamName":prop.event.homeTeamName,
                    "HomeTeamOdds":prop.event.homeTeamOdds,
                    "Sports":prop.event.sports,
                    "BetTeamOdds":prop.odds,
                    "BetTeamName":prop.team,
                    "Prime":"",
                },
                "UserName":localStorage.getItem("userName"),
                "EventId":prop.event.id,
                "Prime":prime
            },{
            headers: {
                Authorization: `Bearer ${localStorage.getItem("token")}`
            }})
            .then(res=>{
                // sendCard()

                
                // console.log(prop.event)
                console.log(res)
                // console.log(prop.odds)
                // console.log(prop.team)
                axios.post("https://localhost:7099/PostMemberDetail",{
                    Name:localStorage.getItem("userName")
                },{
                    headers: {
                        Authorization: `Bearer ${localStorage.getItem("token")}`
                    }
                }).then(res=>{
                    console.log(res)
                    localStorage.removeItem("userMoney")
                    localStorage.removeItem("userProfit")
                    localStorage.setItem("userMoney", res.data.money)
                    localStorage.setItem("userProfit", res.data.profit)
                    console.log(localStorage)
                    router.push({path:"/index/myBet"})
                    location.reload();
                }).catch(err=>{
                    console.log(err)
                    router.push({path:"/index/myBet"})
                    location.reload();
                })
            }).catch(err=>{
                console.log(err)
                localStorage.removeItem("token")
                router.push({path:"/signin"})
            })

            // betResultButton()
            // send prime to your server, to pay with Pay by Prime API .

            
            // Pay By Prime Docs: https://docs.tappaysdk.com/tutorial/zh/back.html#pay-by-prime-api
        })
    }else{
        console.log("cancel bet")
    }


    
    // console.log({Money:betMoney.value, 
    //         DateTIme:getCurrentDateInFormat(), 
    //         Event:{
    //             Id:prop.event.id,
    //             Sports:prop.event.sports,
    //             DateTIme:prop.event.dateTime,
    //             AwayTeamName:prop.event.awayTeamName,
    //             AwayTeamOdds:prop.event.awayTeamOdds,
    //             HomeTeamName:prop.event.homeTeamName,
    //             HomeTeamOdds:prop.event.homeTeamOdds
    //         }})
    emit("betResultEmit", display)
}

let getCurrentDateInFormat = () => {
    const now = new Date();

    const year = now.getFullYear();
    const month = String(now.getMonth() + 1).padStart(2, '0');  // Month is 0-indexed, so we add 1
    const day = String(now.getDate()).padStart(2, '0');
    const hour = String(now.getHours()).padStart(2, '0');
    const seconds = String(now.getSeconds()).padStart(2, '0');

    return `${year}-${month}-${day}T${hour}:${seconds}:00Z`;
}

let updateBetMoney = (event) => {
    betMoney.value = event.target.value;
}

let betMoney = ref(100);

let updateBetPoint = (event) =>{
    betPoint.value = event.target.value;
}

let betPoint = ref(0);

let sendCard = (event)=>{
    // event.preventDefault()

    // 取得 TapPay Fields 的 status
    const tappayStatus = TPDirect.card.getTappayFieldsStatus()

    // 確認是否可以 getPrime
    if (tappayStatus.canGetPrime === false) {
        alert('can not get prime')
        return
    }

    // Get prime
    TPDirect.card.getPrime((result) => {
        if (result.status !== 0) {
            alert('get prime error ' + result.msg)
            return
        }
        alert('get prime 成功，prime: ' + result.card.prime)
        console.log(result)
        // betResultButton()
        // send prime to your server, to pay with Pay by Prime API .

        
        // Pay By Prime Docs: https://docs.tappaysdk.com/tutorial/zh/back.html#pay-by-prime-api
    })
}

// Display ccv field
let fields = {
    number: {
        // css selector
        element: '#card-number',
        placeholder: '**** **** **** ****'
    },
    expirationDate: {
        // DOM object
        element: '#card-expiration-date',
        placeholder: 'MM / YY'
    },
    ccv: {
        element: '#card-ccv',
        placeholder: 'ccv'
    }
};


onMounted(() => {
  // 这里的代码将会在组件挂载后执行

  // 例如，你可以在这里进行初始化操作，或者触发某些逻辑
  console.log('Component has been mounted');


  TPDirect.card.setup({
    fields: fields,
    styles: {
        // Style all elements
        'input': {
            'color': 'gray'
        },
        // Styling ccv field
        'input.ccv': {
            // 'font-size': '16px'
        },
        // Styling expiration-date field
        'input.card-expiration-date': {
            // 'font-size': '16px'
        },
        // Styling card-number field
        'input.card-number': {
            // 'font-size': '16px'
        },
        // style focus state
        ':focus': {
            // 'color': 'black'
        },
        // style valid state
        '.valid': {
            'color': 'green'
        },
        // style invalid state
        '.invalid': {
            'color': 'red'
        },
        // Media queries
        // Note that these apply to the iframe, not the root window.
        '@media screen and (max-width: 400px)': {
            'input': {
                'color': 'orange'
            }
        }
    },
    // 此設定會顯示卡號輸入正確後，會顯示前六後四碼信用卡卡號
    isMaskCreditCardNumber: true,
    maskCreditCardNumberRange: {
        beginIndex: 6,
        endIndex: 11
    }
})
TPDirect.card.onUpdate(function (update) {
    // update.canGetPrime === true
    // --> you can call TPDirect.card.getPrime()
    if (update.canGetPrime) {
        // Enable submit Button to get prime.
        // submitButton.removeAttribute('disabled')
    } else {
        // Disable submit Button to get prime.
        // submitButton.setAttribute('disabled', true)
    }

    // cardTypes = ['mastercard', 'visa', 'jcb', 'amex', 'unionpay','unknown']
    if (update.cardType === 'visa') {
        // Handle card type visa.
    }

    // number 欄位是錯誤的
    if (update.status.number === 2) {
        // setNumberFormGroupToError()
    } else if (update.status.number === 0) {
        // setNumberFormGroupToSuccess()
    } else {
        // setNumberFormGroupToNormal()
    }

    if (update.status.expiry === 2) {
        // setNumberFormGroupToError()
    } else if (update.status.expiry === 0) {
        // setNumberFormGroupToSuccess()
    } else {
        // setNumberFormGroupToNormal()
    }

    if (update.status.ccv === 2) {
        // setNumberFormGroupToError()
    } else if (update.status.ccv === 0) {
        // setNumberFormGroupToSuccess()
    } else {
        // setNumberFormGroupToNormal()
    }
})

let betResultPopup = ref(true)


});
</script>

<template>
  <div class="betResult">
    <div class="betResult-content">
        <div class="betResult-content-table">
            <table>
                <tr>
                    <th>投注金額</th>
                    <th>賠率</th>
                    <th style="width:60%">投注隊伍</th>
                    <!-- <th>讓分</th> -->
                </tr>
                <tr>
                    <td>{{betMoney}}</td>
                    <td>{{ odds }}</td>
                    <td>{{ team }}</td>
                    <!-- <td><input type="number" v-on:input="updateBetPoint"></td> -->
                </tr>
            </table>
        </div>
        <div style="display: flex;">
            <input type="range" class="betmoneyinputrange" min="100" max="5000" step="100" value="50" v-on:input="updateBetMoney">
        </div>
        <div style="display: flex;margin-top: 30px;">
            <div style="display: flex;flex-direction: column;margin: auto;">
                <div style="margin: auto; color: #DDDDDD;">信用卡資料</div>
                <div class="tpfield" id="card-number"></div>
                <div class="tpfield" id="card-expiration-date"></div>
                <div class="tpfield" id="card-ccv"></div>
                <div v-on:click="sendCard" style="display: flex;" v-show="false">
                    <div style="margin: auto;border: 1px solid gray;padding: 5px 10px;">確認</div>
                </div>
            </div>
        </div>
        <div class="betResult-content-surebutton">
            <div class="betResult-content-surebutton-content">
                <div class="betResult-content-surebutton-text" v-on:click="betResultButton(false, true)">確認</div>
                <div class="betResult-content-surebutton-textcancel" v-on:click="betResultButton(false, false)">取消</div>
            </div>
        </div>
    </div>
    <div class="betResult-popup" v-show="betResultPopup">
        <div class="betResult-popup-window">
            <div class="betResult-popup-window-header"></div>
            <div class="betResult-popup-window-body">
                <div style="color: #fff;text-align: center;margin-top: 100px;margin-bottom: 100px; cursor: default;">下注成功!</div>
                <div class="betResult-popup-window-body-button">
                    <div class="betResult-popup-window-body-button-body">確認</div>
                </div>
            </div>
        </div>
    </div>
  </div>
</template>

<style scoped>
.betResult {
  border-top: 2px solid #367a65;
  background-color: #383838;
  position: absolute;
  z-index: 100;
  color: black;
  top: 0;
  width: 100%;
  height: 100%;
}

.betResult-content{
    /* background-image: linear-gradient(rgba(12,22,20,.1),transparent 20px),radial-gradient(122% 370px at center -220px,#009969 0,transparent 100%),linear-gradient(to right bottom,#0c1614,#084436); */
}

.betResult-content-table{
    margin-top: 30px;
    width: 100%;
}

.betResult-content-table > table{
    width: 90%;
    margin: auto;
}

.betResult-content-table  table tr th{
    width: 25%;
    text-align: center;
    color: #DDDDDD;
    background-color: #222;
    padding: 5px 0;
}

.betResult-content-table  table tr td{
    text-align: center;
    color: #DDDDDD;
    padding: 5px 0;
}


.betResult-content-surebutton{  
    margin-top: 30px;
    color: #fff;
    display: flex;
}

.betResult-content-surebutton-content{
    margin: auto;
    display: flex;
}

.betResult-content-surebutton-text{
    background-color: #126e51;
    padding: 5px 10px;
    border-radius: 3px;
    cursor: pointer;
}

.betResult-content-surebutton-textcancel{
    padding: 5px 10px;
    cursor: pointer;
    margin-left: 20px;
}



.betmoneyinputrange {
  -webkit-appearance: none;
  appearance: none;
  background: transparent;
  cursor: pointer;
  width: 88%;
  margin: auto;
  margin-top: 30px;
}

.betmoneyinputrange::-webkit-slider-thumb{
    -webkit-appearance: none;
    height: 10px;
    width: 10px;
    border-radius: 50%;
    background-color: #fff;
}

.betmoneyinputrange::-webkit-slider-runnable-track {
    /* background-color: #126e51; */
  background: #367a65;
  height: 0.5rem;
  border-radius: 5px;
}

.tpfield {
    height: 40px;
    width: 300px;
    border: 1px solid gray;
    margin: 5px 0;
    padding: 5px;
}

.betResult-popup{
    position: absolute;
    width: 100%;
    height: 100%;
    z-index: 99;
    top: 0;
    background-color: #3838385c;
}

.betResult-popup-window{
    width: 500px;
    height:300px;
    background: #383838;
    border-radius: 5px;
    position: fixed;
    left: 40%;
    bottom: 15%;
}

.betResult-popup-window-header{
    width: 100%;
    height: 30px;
    background: #126e51;
    border-top-left-radius: 5px;
    border-top-right-radius: 5px;
}

.betResult-popup-window-body{
    width: 100%;
    height: calc(100% - 30px);
}

.betResult-popup-window-body-button{
    margin: auto;
    width: 50px;
    text-align: center;
}

.betResult-popup-window-body-button-body{
    padding: 5px;
    color: #fff;
    background: #126e51;
    border-radius: 5px;
    cursor: pointer;
}
</style>
