<script>
import { onMounted, ref } from 'vue';
import { reactive } from 'vue';
import axios from 'axios'
import router from "../../../router/router"

export default{
    setup() {
        let sendCard = (event)=>{
            event.preventDefault()

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

        onMounted(()=>{
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
        })
        return {sendCard}
    },
    components: {  }
}
</script>

<template>
    <div style="display: flex;">
        <div style="display: flex;flex-direction: column;margin: auto;">
            <div class="tpfield" id="card-number"></div>
            <div class="tpfield" id="card-expiration-date"></div>
            <div class="tpfield" id="card-ccv"></div>
            <div v-on:click="sendCard" style="display: flex;">
                <div style="margin: auto;border: 1px solid gray;padding: 5px 10px;">確認</div>
            </div>
        </div>
    </div>
</template>

<style>
.tpfield {
    height: 40px;
    width: 300px;
    border: 1px solid gray;
    margin: 5px 0;
    padding: 5px;
}
</style>