import React from 'react';
import axios from 'axios';

function RequestWithAxios(props){
    axios({url:props.url,method:props.method, data:props.data})
    .then(res => 
        {
          console.log(res);
            if(res.status!==200){
                throw ("Error");
            }
            setTypeAlert("success");
            if(typeof(props.callback)=="function"){
              props.callback();  
            }
            return res;
        })
    .catch(error => {
      console.log(error);
      setTypeAlert("error");
    });
}
export default RequestWithAxios;