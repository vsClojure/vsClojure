(ns %namespace%
  (:import [System.Windows.Forms Form Button])
  (:import [System.Drawing Size Point])
)

(defn create%namespace% []
  (let [form (Form.)
	    btnClose (Button.)
       ]
    
    (doto btnClose
      (.set_Name "btnClose")
      (.set_Location (Point. 50 50))
      (.set_Text "Close")
      (.add_Click 
        (gen-delegate EventHandler [sender args]
	      (.Close form)
        )
      )
    )

    (doto (.Controls form)
      (.Add btnClose)
    )
    
    (doto form
      (.set_Text "%namespace%")
      (.set_Size (Size. 200 150))
    )

    form
  )
)