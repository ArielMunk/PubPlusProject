import * as React from 'react';
import { DataGrid } from '@mui/x-data-grid';

export default function DataTable(props) {

    const getRowClassName = (params) => {
        // Access the row data using params.row
        const status_id = params.row.status_id;
    
        // Add your conditions to determine the class name
        if (status_id ==3) {
          return 'gray-bg';
        }
    
        // If none of the conditions match, return an empty string
        return '';
      };

    return (
      <div style={{ height: 400, width: '100%' }}>
        <DataGrid
          rows={props.data.rows}
          columns={props.data.columns}
          initialState={{
            pagination: {
              paginationModel: { page: 0, pageSize: 5 },
            },
            columns: {
                columnVisibilityModel: {
                  status_id: false,
                },
              },
          }}
          pageSizeOptions={[5, 10]}
          getRowClassName={getRowClassName}
        />
      </div>
    );
  }