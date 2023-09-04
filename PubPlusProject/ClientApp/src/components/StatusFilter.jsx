import React from 'react';
import axios from "axios";
import MultiSelect from "./MultiSelect";
import { Button } from '@mui/material';

export default function StatusFilter(props){
    const [genericStatuses,setGenericStatuses] = React.useState([]);
    React.useEffect(()=>{
        axios.get("list/statuses")
        .then(res => 
            {
                if(res.status!==200){
                    throw ("Error");
                }
                setGenericStatuses(res.data);
            })
        .catch(error => {
        console.log(error);
        })},[]);

        function ChangeHandler(value){
            axios.get(["user/get-all-users?statuses=",value.target.value].join(""))
            .then(res => 
                {
                    if(res.status!==200){
                        throw ("Error");
                    }
                    props.setRows(JSON.parse(res.data));
                })
            .catch(error => {console.log(error)});
        }
        return (
        <React.Fragment>
            <MultiSelect options={{"names":genericStatuses, "multiSelect":true,"ChangeHandler":ChangeHandler,"title":"Filter With Statuses"}} />
            {/*<Button onClick={ChangeHandler}>Reset</Button>*/}
        </React.Fragment>
        );
}