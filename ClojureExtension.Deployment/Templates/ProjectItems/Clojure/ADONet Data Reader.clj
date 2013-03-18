(ns %namespace%)

(System.Reflection.Assembly/LoadWithPartialName "System.Data")

(defn read%namespace% [reader]
  (loop [columnAccumulator {} columnIndex 0]
    (if (>= columnIndex (.FieldCount reader))
      columnAccumulator
      (recur (assoc columnAccumulator (.GetName reader columnIndex) (.GetValue reader columnIndex)) (inc columnIndex))
    )
  )
)

(defn readRecursive []
  (let [ connection (System.Data.SqlClient.SqlConnection. "Data Source=localhost;Initial Catalog=AdventureWorks2008R2;Integrated Security=true;")
         cmd (System.Data.SqlClient.SqlCommand. "SELECT TOP 10 BusinessEntityID, FirstName + ' ' + LastName AS FullName FROM Person.Person" connection)
         _ (.Open connection)
         reader (.ExecuteReader cmd)
       ]
    (def rows
      (if (.Read reader)
        (loop [rowAccumulator [(readRow reader)]]
          (if (not (.Read reader))
            rowAccumulator
            (recur (conj rowAccumulator (readRow reader)))
          )
        )
        []
      )
    )

    (.Close reader) 
    (.Close connection)
    rows
  )
)
