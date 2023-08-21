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
                path:'/backendManage',
                component:()=>import("../pages/PageView/BackendManage.vue")
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
]

export default routes;