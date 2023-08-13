const routes = [
    {
        path:"/index",
        component:()=>import("../pages/index.vue"),
        children:[
            {
                path: 'basketball',
                component: ()=>import("../pages/sports/basketball/basketball.vue")
            },
        ]
    },
    {
        path:"/signup",
        component:()=>import("../pages/signup.vue")
    },
]

export default routes;