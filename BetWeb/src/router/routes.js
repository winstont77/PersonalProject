const routes = [
    {
        path:"/index",
        component:()=>import("../pages/index.vue"),
        children:[
            // {
            //     path: 'basketball',
            //     component: ()=>import("../pages/sports/basketball/basketball.vue")
            // },
            {
                path: 'sport',
                component: ()=>import("../pages/PageView/SportBet.vue"),
                children:[
                    {
                        path: 'basketball',
                        component: ()=>import("../pages/sports/basketball/basketball.vue")
                    }
                ]
            },
            {
                path: 'myBet',
                component: ()=>import("../pages/PageView/MyBet.vue")
            },
            {
                path: "tappay",
                component: ()=>import("../pages/PageView/tappay/tappay.vue")
            }
        ]
    },
    {
        path:"/signup",
        component:()=>import("../pages/signup.vue")
    },
    {
        path:"/signin",
        component:()=>import("../pages/signin.vue")
    },
    {
        path:"/backendManager/signin",
        component:()=>import("../backendManager/signin.vue")
    },
    /*後臺路由*/
    {
        path:"/backendManager/index",
        component:()=>import("../backendManager/index.vue"),
        children:[
            {
                path:"dashboard",
                component:()=>import("../backendManager/content/dashboard.vue")
            },
            {
                path:"tablelist",
                component:()=>import("../backendManager/content/tablelist.vue")
            }
        ]
    },
]

export default routes;