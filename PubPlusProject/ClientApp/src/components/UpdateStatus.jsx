import React,{useState,useEffect} from "react";
import axios from "axios";
import MultiSelect from "./MultiSelect";
function UpdateStatus(props){

    const statuses = props.genericStatuses;
    const statusSelected = props.selected;
    function ChangeHandler(e){
        const data = new FormData();
        data.append("status",e.target.value);
        axios.post("user/change-status",data)
        .then(res => 
            {
                console.log(res);
                if(res.status!==200){
                    throw ("Error");
                }
                props.ChangeHandler(res.data);
            })
        .catch(error => {
        console.log(error);
        });
    }
if(typeof(statuses)!="undefined"){
    return (
        <MultiSelect options={{"names":statuses, "multiSelect":false,"ChangeHandler":ChangeHandler,"selected":statusSelected,"title":"Change Your Status"}} />
    );
}
else{
    return (<span></span>);
}
    
}
export default UpdateStatus;