using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace Miracom.UI.Controls.MCTreeView
{
	/// <summary>
	/// Summary description for MCTreeView.
	/// </summary>
	public class MCTreeView : System.Windows.Forms.TreeView
	{
        protected struct Node_Color_Tag
        {
            public Color fore;
            public Color back;
        }
        protected ArrayList     m_selectNodeColor; 

		protected ArrayList		m_selectNode;
		protected TreeNode		m_lastNode, m_firstNode;

		public MCTreeView()
		{
			m_selectNode = new ArrayList();
            m_selectNodeColor = new ArrayList();
		}

        //UserControl은 Dispose를 재정의하여 구성 요소 목록을 정리합니다.
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                m_selectNodeColor.Clear();
                m_selectNodeColor = null;

                m_selectNode.Clear();
                m_selectNode = null;

                m_lastNode = null;
                m_firstNode = null;
            }

            base.Dispose(disposing);
        }
        
        protected override void OnPaint(PaintEventArgs pe)
		{
			// TODO: Add custom paint code here

			// Calling the base class OnPaint
			base.OnPaint(pe);
		}


		public ArrayList SelectedNodes
		{
			get
			{
				return m_selectNode;
			}
			set
			{
                try
                {
				    removePaintFromNodes();
				    m_selectNode.Clear();
				    m_selectNode = value;

                    m_selectNodeColor.Clear();
                    Node_Color_Tag node_color;
                    foreach (TreeNode n in m_selectNode)
                    {
                        node_color.fore = n.ForeColor;
                        node_color.back = n.BackColor;
                        m_selectNodeColor.Add(node_color);
                    }

                    paintSelectedNodes();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
			}
		}



// Triggers
//
// (overriden method, and base class called to ensure events are triggered)


		protected override void OnBeforeSelect(TreeViewCancelEventArgs e)
		{
			base.OnBeforeSelect(e);
				
			bool bControl = (ModifierKeys==Keys.Control);
			bool bShift = (ModifierKeys==Keys.Shift);

            try
            {

			    // selecting twice the node while pressing CTRL ?
			    if (bControl && m_selectNode.Contains( e.Node ) )
			    {
				    // unselect it (let framework know we don't want selection this time)
				    e.Cancel = true;
    	
				    // update nodes
				    removePaintFromNodes();

                    int i_index = m_selectNode.IndexOf(e.Node);
                    m_selectNodeColor.RemoveAt(i_index);

				    m_selectNode.Remove( e.Node );
				    paintSelectedNodes();
				    return;
			    }

			    m_lastNode = e.Node;
			    if (!bShift) m_firstNode = e.Node; // store begin of shift sequence

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


		protected override void OnAfterSelect(TreeViewEventArgs e)
		{
			base.OnAfterSelect(e);

			bool bControl = (ModifierKeys==Keys.Control);
			bool bShift = (ModifierKeys==Keys.Shift);

            try
            {
			    if (bControl)
			    {
				    if ( !m_selectNode.Contains( e.Node ) ) // new node ?
				    {
					    m_selectNode.Add( e.Node );

                        Node_Color_Tag node_color;
                        node_color.fore = e.Node.ForeColor;
                        node_color.back = e.Node.BackColor;
                        m_selectNodeColor.Add(node_color);

				    }
				    else  // not new, remove it from the collection
				    {
					    removePaintFromNodes();

                        int i_index = m_selectNode.IndexOf(e.Node);
                        m_selectNodeColor.RemoveAt(i_index);

					    m_selectNode.Remove( e.Node );
				    }
				    paintSelectedNodes();
			    }
			    else 
			    {
				    // SHIFT is pressed
				    if (bShift)
				    {
					    Queue myQueue = new Queue();
    					
					    TreeNode uppernode = m_firstNode;
					    TreeNode bottomnode = e.Node;
					    // case 1 : begin and end nodes are parent
					    bool bParent = isParent(m_firstNode, e.Node); // is m_firstNode parent (direct or not) of e.Node
					    if (!bParent)
					    {
						    bParent = isParent(bottomnode, uppernode);
						    if (bParent) // swap nodes
						    {
							    TreeNode t = uppernode;
							    uppernode = bottomnode;
							    bottomnode = t;
						    }
					    }
					    if (bParent)
					    {
						    TreeNode n = bottomnode;
						    while ( n != uppernode.Parent)
						    {
							    if ( !m_selectNode.Contains( n ) ) // new node ?
								    myQueue.Enqueue( n );

							    n = n.Parent;
						    }
					    }
						    // case 2 : nor the begin nor the end node are descendant one another
					    else
					    {
						    if ( (uppernode.Parent==null && bottomnode.Parent==null) || (uppernode.Parent!=null && uppernode.Parent.Nodes.Contains( bottomnode )) ) // are they siblings ?
						    {
							    int nIndexUpper = uppernode.Index;
							    int nIndexBottom = bottomnode.Index;
							    if (nIndexBottom < nIndexUpper) // reversed?
							    {
								    TreeNode t = uppernode;
								    uppernode = bottomnode;
								    bottomnode = t;
								    nIndexUpper = uppernode.Index;
								    nIndexBottom = bottomnode.Index;
							    }

							    TreeNode n = uppernode;
							    while (nIndexUpper <= nIndexBottom)
							    {
								    if ( !m_selectNode.Contains( n ) ) // new node ?
									    myQueue.Enqueue( n );
    								
								    n = n.NextNode;

								    nIndexUpper++;
							    } // end while
    							
						    }
						    else
						    {
							    if ( !m_selectNode.Contains( uppernode ) ) myQueue.Enqueue( uppernode );
							    if ( !m_selectNode.Contains( bottomnode ) ) myQueue.Enqueue( bottomnode );
						    }
					    }

					    m_selectNode.AddRange( myQueue );

                        Node_Color_Tag node_color;
                        foreach (TreeNode n1 in myQueue)
                        {
                            node_color.fore = n1.ForeColor;
                            node_color.back = n1.BackColor;
                            m_selectNodeColor.Add(node_color);
                        }

					    paintSelectedNodes();
					    m_firstNode = e.Node; // let us chain several SHIFTs if we like it
				    } // end if m_bShift
				    else
				    {
					    // in the case of a simple click, just add this item
					    if (m_selectNode!=null && m_selectNode.Count>0)
					    {
						    removePaintFromNodes();
						    m_selectNode.Clear();

                            m_selectNodeColor.Clear();
					    }
					    m_selectNode.Add( e.Node );

                        Node_Color_Tag node_color;
                        node_color.fore = e.Node.ForeColor;
                        node_color.back = e.Node.BackColor;
                        m_selectNodeColor.Add(node_color);

				    }
			    }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }



// Helpers
//
//


		protected bool isParent(TreeNode parentNode, TreeNode childNode)
		{
			if (parentNode==childNode)
				return true;

			TreeNode n = childNode;
			bool bFound = false;
			while (!bFound && n!=null)
			{
				n = n.Parent;
				bFound = (n == parentNode);
			}
			return bFound;
		}

		protected void paintSelectedNodes()
		{
            try
            {
			    foreach ( TreeNode n in m_selectNode )
			    {
				    n.BackColor = SystemColors.Highlight;
				    n.ForeColor = SystemColors.HighlightText;
			    }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
		}

		protected void removePaintFromNodes()
		{
            try
            {
                if (m_selectNode.Count < 1) return;

                TreeNode n;
                Node_Color_Tag node_color;
                for (int i = 0; i < m_selectNode.Count; i++)
                {
                    n = (TreeNode)m_selectNode[i];
                    node_color = (Node_Color_Tag)m_selectNodeColor[i];

                    n.BackColor = node_color.back;
                    n.ForeColor = node_color.fore;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
		}

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // MCTreeView
            // 
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ResumeLayout(false);

        }

	}
}
