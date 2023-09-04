import * as React from 'react';
import { GridColDef, GridValueGetterParams } from '@mui/x-data-grid';
import DataTable from './DataTable';
import axios from 'axios';
import StatusFilter from './StatusFilter';

export default function UsersDataTable(props) {
  React.useEffect(()=>{
    axios.get("user/get-all-users")
    .then(res => 
        {
            if(res.status!==200){
                throw ("Error");
            }
            props.options.setRows(JSON.parse(res.data));
        })
    .catch(error => {console.log(error);
    })},[]);

    const columns = [
        { field: 'id', headerName: 'ID', width: 70 },
        { field: 'first_name', headerName: 'First name', width: 130 },
        { field: 'last_name', headerName: 'Last name', width: 130 },
        { field: 'status_name', headerName: 'Status',width: 130},
        { field: 'status_id',options: {
          display: false,
        } }
      ];
      

      return (
        <React.Fragment>
          <StatusFilter setRows={(value)=>props.options.setRows(value)}/>
          <DataTable data={{rows:props.options.rows,columns:columns}} />
        </React.Fragment>
      )
}